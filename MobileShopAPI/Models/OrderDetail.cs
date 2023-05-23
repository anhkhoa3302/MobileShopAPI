﻿using System;
using System.Collections.Generic;

namespace MobileShopAPI.Models
{
    public partial class OrderDetail
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string OrderId { get; set; } = null!;
        public int Quantity { get; set; }
        public long? TotalPrice { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
