using System;
using System.Collections.Generic;

namespace MobileShopAPI.Models
{
    /// <summary>
    /// Actions on website using coin
    /// </summary>
    public partial class CoinAction
    {
        public CoinAction()
        {
            InternalTransactions = new HashSet<InternalTransaction>();
        }

        public long Id { get; set; }
        public string ActionName { get; set; } = null!;
        public string? Description { get; set; }
        public int? CaCoinAmount { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<InternalTransaction> InternalTransactions { get; set; }
    }
}
