using Microsoft.AspNetCore.Identity;

namespace MobileShopAPI.ViewModel
{
    public class UserViewModel
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public int? Status { get; set; }
        public string? avatar_url { get; set; }
    }
}
