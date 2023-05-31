using System;
using System.Collections.Generic;

namespace MobileShopAPI.Models
{
    public partial class Product
    {
        public Product()
        {
            Images = new HashSet<Image>();
            ProductOrders = new HashSet<ProductOrder>();
            UserRatings = new HashSet<UserRating>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Stock { get; set; }
        public long Price { get; set; }
        public int? Status { get; set; }
        public long CategoryId { get; set; }
        public long BrandId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UserId { get; set; } = null!;
        /// <summary>
        /// part of primaryKey
        /// </summary>
        public long SizeId { get; set; }
        /// <summary>
        /// part of primaryKey
        /// </summary>
        public long ColorId { get; set; }
        public int? Priorities { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Brand Brand { get; set; } = null!;
        public virtual Category Category { get; set; } = null!;
        public virtual Color Color { get; set; } = null!;
        public virtual Size Size { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
        public virtual ICollection<UserRating> UserRatings { get; set; }
    }
}
