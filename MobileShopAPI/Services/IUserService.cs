using Microsoft.AspNetCore.Identity;
using MobileShopAPI.Helpers;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;

namespace MobileShopAPI.Services
{
    public interface IUserService
    {

        Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model);



    }


    public class UserService : IUserService
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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
    }
}
