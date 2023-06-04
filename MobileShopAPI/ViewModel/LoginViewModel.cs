using System.ComponentModel.DataAnnotations;

namespace MobileShopAPI.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
