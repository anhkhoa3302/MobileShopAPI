namespace MobileShopAPI.ViewModel
{
    public class UserRatingViewModel
    {
        public short Rating { get; set; }
        public string? Comment { get; set; }
        public long ProductId { get; set; }
        public string UsderId { get; set; } = null!;
    }
}
