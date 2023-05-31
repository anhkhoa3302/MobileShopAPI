namespace MobileShopAPI.ViewModel
{
    public class ImageViewModel
    {
        public long Id { get; set; }
        /// <summary>
        /// url hình ảnh
        /// </summary>
        public string? Url { get; set; }
        /// <summary>
        /// hình ảnh là ảnh bìa
        /// </summary>
        public bool IsCover { get; set; }
    }
}
