using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace MobileShopAPI.Models
{
    public class ApplicationUser:IdentityUser
    {
        public ApplicationUser()
        {
            Orders = new HashSet<Order>();
            Products = new HashSet<Product>();
            Transactions = new HashSet<Transaction>();
            UserRatings = new HashSet<UserRating>();
        }
        [PersonalData]
        public string? FirstName { get; set; }
        [PersonalData]
        public string? MiddleName { get; set; }
        [PersonalData]
        public string? LastName { get; set; }
        [PersonalData]
        public string? Address { get; set; }
        [PersonalData]
        public int? Status { get; set; }
        [PersonalData]
        public string? avatar_url { get; set; }
        [PersonalData]
        [Column("createdDate")]
        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<UserRating> UserRatings { get; set; }
    }
}
