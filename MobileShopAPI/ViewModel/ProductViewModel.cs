namespace MobileShopAPI.ViewModel
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Stock { get; set; }
        public long Price { get; set; }
        public int? Status { get; set; }
        public long CategoryId { get; set; }
        public long BrandId { get; set; }
        public string UserId { get; set; } = null!;
        /// <summary>
        /// part of primaryKey
        /// </summary>
        public long SizeId { get; set; }
        /// <summary>
        /// part of primaryKey
        /// </summary>
        public long ColorId { get; set; }

        public IEnumerable<ImageViewModel> Images { get; set; } = null!;
    }
}
