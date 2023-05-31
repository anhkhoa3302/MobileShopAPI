﻿using Microsoft.AspNetCore.Identity;
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
            Reports = new HashSet<Report>();
            ShippingAddresses = new HashSet<ShippingAddress>();
            Transactions = new HashSet<Transaction>();
            UserRatings = new HashSet<UserRating>();
            InternalTransactions = new HashSet<InternalTransaction>();
            ActiveSubscriptions = new HashSet<ActiveSubscription>();
        }
        [PersonalData]
        public string? FirstName { get; set; }
        [PersonalData]
        public string? MiddleName { get; set; }
        [PersonalData]
        public string? LastName { get; set; }
        [PersonalData]
        public int? Status { get; set; }
        [PersonalData]
        public string? AvatarUrl { get; set; }
        [PersonalData]
        public long? UserBalance { get; set; }
        [PersonalData]
        [Column("createdDate")]
        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<UserRating> UserRatings { get; set; }
        public virtual ICollection<ActiveSubscription> ActiveSubscriptions { get; set; }
        public virtual ICollection<InternalTransaction> InternalTransactions { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<ShippingAddress> ShippingAddresses { get; set; }
    }
}
