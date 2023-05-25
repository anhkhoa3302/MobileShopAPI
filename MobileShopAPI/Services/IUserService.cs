using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MobileShopAPI.Helpers;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MobileShopAPI.Services
{
    public interface IUserService
    {

        Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model);

        Task<UserManagerResponse> LoginUserAsync(LoginViewModel model);

    }


    public class UserService : IUserService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public UserService(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model)
        {
            if (model == null)
                throw new NullReferenceException("Register model is null");

            if(model.Password != model.ConfirmPassword)
            {
                return new UserManagerResponse
                {
                    Message = "Password don't match!",
                    isSuccess = false
                };
            }

            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email
            };
            var result = await _userManager.CreateAsync(user,model.Password);

            if(result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "User created successfully",
                    isSuccess = true
                };
            }


            return new UserManagerResponse
            {
                Message = "Can't create user",
                isSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<UserManagerResponse> LoginUserAsync(LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if(user == null)
            {
                return new UserManagerResponse
                {
                    Message = "There is no user with this email address",
                    isSuccess = false
                };
            }

            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if(!result)
            {
                return new UserManagerResponse
                {
                    Message = "Wrong password",
                    isSuccess = false
                };
            }

            //Claim array
            var claim = new[]
            {
                new Claim("Email", model.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            //Key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSetting:Key"]));

            //Token
            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSetting:Issuer"],
                audience: _configuration["AuthSetting:Audience"],
                claims: claim,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key,SecurityAlgorithms.HmacSha256)
                );

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
            return new UserManagerResponse
            {
                Message = "Login successfully",
                isSuccess = true,
                Token = tokenAsString,
                ExpireDate = token.ValidTo
            };
        }
    }
}
