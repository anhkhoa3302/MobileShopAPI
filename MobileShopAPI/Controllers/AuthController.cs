using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileShopAPI.Services;

namespace MobileShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }


    }
}
