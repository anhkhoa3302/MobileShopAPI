using System.ComponentModel.DataAnnotations;

namespace MobileShopAPI.ViewModel
{
    public class ColorViewModel
    {
        [Required]
        public string ColorName { get; set; } = null!;
        [Required]
        public string HexValue { get; set; } = null!;
    }
}
