using System.ComponentModel.DataAnnotations;

namespace MobileShopAPI.ViewModel
{
    public class ReportCategoryViewModel
    {
        [Required]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}
