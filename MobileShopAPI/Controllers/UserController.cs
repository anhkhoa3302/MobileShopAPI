using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileShopAPI.Services;
using MobileShopAPI.ViewModel;

namespace MobileShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IUserManagerService _userManagerService;

        public UserController(IUserManagerService userManagerService)
        {
            _userManagerService = userManagerService;
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllUser()
        {
            var userList = await _userManagerService.GetAllUserAsync();
            return Ok(userList);
        }
        [HttpGet("getProfile")]
        public async Task<IActionResult> GetUserProfile(string userId)
        {
            var result = await _userManagerService.GetUserProfileAsync(userId);
            if (result == null) { return BadRequest(result); }
            return Ok(result);
        }

        [HttpPut("updateProfile")]
        public async Task<IActionResult> UpdateUserProfile(string userId,UpdateUserProfileViewModel model)
        {
            var result = await _userManagerService.UpdateUserProfileAsync(userId, model);
            if (result == null) { return BadRequest(); }
            if(result.isSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("ban")]
        public async Task<IActionResult> BanUser(string userId)
        {
            var result = await _userManagerService.BanUser(userId);
            if (result == null) { return BadRequest(); }
            if (result.isSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
