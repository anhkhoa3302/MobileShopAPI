using System.ComponentModel.DataAnnotations;

namespace MobileShopAPI.Helpers
{
    public class BrandViewModelcs
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime? CreatedDate { get; set; }

    }
}
