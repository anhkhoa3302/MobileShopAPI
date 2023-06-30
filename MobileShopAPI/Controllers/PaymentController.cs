using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MobileShopAPI.Data;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.Services;
using MobileShopAPI.ViewModel;

namespace MobileShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly IVnPayService _vnPayService;
        private readonly UserManager<ApplicationUser> _user;
        private readonly ApplicationDbContext _context;
        private readonly EmailService.IEmailSender _emailSender;
        private readonly IUserManagerService _userManagerService ;
        public PaymentController(IVnPayService vnPayService, ApplicationDbContext context,UserManager<ApplicationUser> userManager, 
            EmailService.IEmailSender emailSender, IUserManagerService userManagerService)
        {
            _vnPayService = vnPayService;
            _context = context;
            _user = userManager;
            _emailSender = emailSender;
            _userManagerService = userManagerService;
        }
        [HttpPost]
        public async Task<IActionResult>CreatePaymentUrl([FromBody]PaymentInformationModel model) 
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);
            return Redirect(url);
        }
        [HttpGet("callback")]
        public async Task<IActionResult>PaymentCallback()
        {
            var user = await _user.GetUserAsync(User);

            var response = _vnPayService.PaymentExecute(Request.Query);
            
            VnpTransaction transaction = new()
            {
                UserId = user.Id,
                Id = response.TransactionId,
                PackageId = response.packageId
                //OrderId = response.OrderId,
            };

            //Order order = await _context.Orders.FindAsync(transaction.OrderId);
            String message = "<p>Xin chào " +
                            "<p><b>Chi tiết đơn hàng</b> :</p>" +
                            //"<p><b>Địa chỉ giao</b> : " + order.Address + "</p>" +
                            //"<p><b>Ngày thanh toán</b> : " + order.CreatedDate + "</p>" +
                            "<p><b>Mã giao dịch</b> : " + transaction.Id + "</p>";
            //EmailService.Message mssg = new EmailService.Message(new string[] { user.Email }, "Chi tiết đơn hàng", message);
            //_emailSender.SendEmailAsync(mssg);

            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            return Json(true);

        }
        //[HttpPost]
        //public async Task<IActionResult> CoinPurse(string id, UpdateUserProfileViewModel model)
        //{
        //    var user = await _user.GetUserAsync(User);
        //    var idpackage = _vnPayService.CoinPurse(id);
        //    var balance = _userManagerService.UpdateUserProfileAsync(user.Id, model);


        //}
        
    }
    


}
