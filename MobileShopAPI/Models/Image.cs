using System;
using System.Collections.Generic;

namespace MobileShopAPI.Models
{
    public partial class Image
    {
        public Image()
        {
            ProductImgs = new HashSet<ProductImg>();
        }

        public long Id { get; set; }
        /// <summary>
        /// url hình ảnh
        /// </summary>
        public long? Url { get; set; }
        /// <summary>
        /// hình ảnh là ảnh bìa
        /// </summary>
        public int? IsCover { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<ProductImg> ProductImgs { get; set; }
    }
}
