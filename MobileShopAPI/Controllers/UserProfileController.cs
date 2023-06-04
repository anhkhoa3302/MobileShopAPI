using Microsoft.AspNetCore.Mvc;

namespace MobileShopAPI.Controllers
{
    public class UserProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
