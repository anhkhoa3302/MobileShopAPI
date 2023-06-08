using System.ComponentModel.DataAnnotations;

namespace MobileShopAPI.ViewModel
{
    public class ProductViewModel
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public long Price { get; set; }
        public int Status { get; set; }
        public bool isHidden { get; set; }
        [Required]
        public long CategoryId { get; set; }
        [Required]
        public long BrandId { get; set; }
        [Required]
        public string? UserId { get; set; }
        [Required]
        /// <summary>
        /// part of primaryKey
        /// </summary>
        public long SizeId { get; set; }
        [Required]
        /// <summary>
        /// part of primaryKey
        /// </summary>
        public long ColorId { get; set; }

        public List<ImageViewModel>? Images { get; set; }

    }
}
