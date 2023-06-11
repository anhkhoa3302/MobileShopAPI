namespace MobileShopAPI.ViewModel
{
    public class SearchViewModel
    {
        public string KeyWord { get; set; } = null!;
        public IEnumerable<Object>? Items { get; set; }
    }
}
