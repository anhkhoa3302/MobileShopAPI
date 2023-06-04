using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileShopAPI.Helpers;
using System.Security.Claims;

namespace MobileShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class testController : ControllerBase
    {

        [HttpGet("AuthorizeTest")]
        [Authorize]
        public async Task<IActionResult> AuthTest()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);
            return Ok(user.Value);
        }

        [HttpGet]
        [Route("getBaseUrl")]
        public string GetBaseUrl()
        {
            var request = HttpContext.Request;

            var baseUrl = $"{request.Scheme}://{request.Host}:{request.PathBase.ToUriComponent()}";

            var confirmEmailUrl = $"{baseUrl}/api/auth/confirmEmail";
            return baseUrl;
        }

        [HttpGet]
        [Route("getUniqueString")]
        public string GetUniqueString()
        {
            return StringIdGenerator.GenerateUniqueId();
        }
    }
}
