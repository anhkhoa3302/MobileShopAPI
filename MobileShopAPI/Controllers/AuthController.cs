﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileShopAPI.Helpers;
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
                    return Ok(result); //Status code: 200
                return BadRequest(result);//Status code: 404
            }
            return BadRequest("Some properies are not valid");//Status code: 404
        }
    }
}