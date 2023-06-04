using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MimeKit.Cryptography;
using MobileShopAPI.Data;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.ViewModel;

namespace MobileShopAPI.Services
{
    public interface IUserManagerService
    {
        Task<List<ApplicationUser>> GetAllUserAsync();
        Task<ApplicationUser?> GetUserProfileAsync(string userId);

        Task<UserManagerResponse?> UpdateUserProfileAsync(string userId,UpdateUserProfileViewModel model);

        Task<UserManagerResponse?> BanUser(string userId);

    }

    public class UserManagerService:IUserManagerService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserManagerService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<UserManagerResponse?> BanUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new UserManagerResponse
                {
                    Message = "User not found",
                    isSuccess = false
                };

            user.Status = 1;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return new UserManagerResponse
            {
                Message = "User has been banned",
                isSuccess = true
            };
        }
        public async Task<List<ApplicationUser>> GetAllUserAsync()
        {
            var userList = await _context.Users.ToListAsync();
            return userList;
        }

        public async Task<ApplicationUser?> GetUserProfileAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) { return null; }
            return user;
        }

        public async Task<UserManagerResponse?> UpdateUserProfileAsync(string userId, UpdateUserProfileViewModel model)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new UserManagerResponse
                {
                    Message = "User not found",
                    isSuccess = false
                };
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.MiddleName = model.MiddleName;
            user.AvatarUrl = model.AvatarUrl;
            user.Description = model.Description;
            user.PhoneNumber = model.PhoneNumber;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return new UserManagerResponse
            {
                Message = "User profile has been updated successfully",
                isSuccess = true
            };
        }
    }
}
