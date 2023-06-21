using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileShopAPI.Responses;
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
        /// <summary>
        /// Get a list of all user
        /// </summary>
        /// <returns></returns>
        /// <response code ="200">List of user</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getAll")]
        [ProducesResponseType(typeof(List<UserViewModel>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllUser()
        {
            var userList = await _userManagerService.GetAllUserAsync();
            return Ok(userList);
        }
        /// <summary>
        /// Get user profile
        /// </summary>
        /// <param name="userId"></param>
        /// <response code ="200">User profile</response>
        /// <response code ="400">Something went wrong</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getProfile")]
        [ProducesResponseType(typeof(UserViewModel), 200)]
        [ProducesResponseType(typeof(UserViewModel), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUserProfile(string userId)
        {
            var result = await _userManagerService.GetUserProfileAsync(userId);
            if (result == null) { return BadRequest(result); }
            return Ok(result);
        }
        /// <summary>
        /// Update user profile
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="model"></param>
        /// <response code ="200">User profile updated</response>
        /// <response code ="400">Something went wrong</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPut("updateProfile")]
        [ProducesResponseType(typeof(UserViewModel), 200)]
        [ProducesResponseType(typeof(UserViewModel), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateUserProfile(string userId,UpdateUserProfileViewModel model)
        {
            var result = await _userManagerService.UpdateUserProfileAsync(userId, model);
            if (result == null) { return BadRequest(); }
            return Ok(result);
        }
        /// <summary>
        /// Ban user account
        /// </summary>
        /// <param name="userId"></param>
        /// <response code ="200">User has been banned</response>
        /// <response code ="400">Something went wrong</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPost("ban")]
        [ProducesResponseType(typeof(UserManagerResponse), 200)]
        [ProducesResponseType(typeof(UserManagerResponse), 400)]
        [ProducesResponseType(500)]
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
