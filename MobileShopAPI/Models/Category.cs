﻿using System;
using System.Collections.Generic;

namespace MobileShopAPI.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        /// <summary>
        /// url hình ảnh
        /// </summary>
        public string? ImageUrl { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
