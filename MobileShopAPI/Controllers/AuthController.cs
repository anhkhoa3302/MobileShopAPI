using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileShopAPI.Services;
using MobileShopAPI.ViewModel;

namespace MobileShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;
        private IMailService _mailService;
        public AuthController(IUserService userService,IMailService mailService)
        {
            _userService = userService;
            _mailService = mailService;
        }

        // api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(model);
                if(result.isSuccess)
                    return Ok(result); //Status code: 200
                return BadRequest(result);//Status code: 404
            }

            return BadRequest("Some properies are not valid");//Status code: 404
        }

        // api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);
                if (result.isSuccess)
                {
                    string message = "<h1>New login noticed</h1>"
                        +"<p>New login to your account at " + DateTime.Now + "</p>";
                    await _mailService.SendEmailAsync(model.Email, "New login", message);
                    return Ok(result); //Status code: 200
                }
                    
                return BadRequest(result);//Status code: 404
            }
            return BadRequest("Some properies are not valid");//Status code: 404
        }
    }
}
