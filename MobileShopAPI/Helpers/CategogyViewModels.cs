namespace MobileShopAPI.Helpers
{
    public class CategogyViewModels
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        /// <summary>
        /// url hình ảnh
        /// </summary>
        public string? ImageUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
