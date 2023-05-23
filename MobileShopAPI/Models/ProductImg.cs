using System;
using System.Collections.Generic;

namespace MobileShopAPI.Models
{
    public partial class ProductImg
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long ImageId { get; set; }

        public virtual Image Image { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
