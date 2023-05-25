namespace MobileShopAPI.Responses
{
    public class BrandManagerResponse
    {
        public string Massage { get; set; }

        public string IsSuccess { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}
