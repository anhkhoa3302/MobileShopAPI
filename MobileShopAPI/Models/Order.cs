using System;
using System.Collections.Generic;

namespace MobileShopAPI.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
            Transactions = new HashSet<Transaction>();
        }

        public string Id { get; set; } = null!;
        public long Total { get; set; }
        public int? Status { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
