using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MobileShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class testController : ControllerBase
    {

        [HttpGet("AuthorizeTest")]
        public async Task<IActionResult> AuthTest()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);
            return Ok(user.Value);
        }
    }
}
