using System.ComponentModel.DataAnnotations;

namespace MobileShopAPI.ViewModel
{
    public class CoinActionViewModel
    {
        [Required]
        public string ActionName { get; set; } = null!;
        public string? Description { get; set; }
        public int? CaCoinAmount { get; set; }

        public int Status { get; set; }
    }
}
