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

        /// <summary>
        /// <example> 10000 or 20000 ....</example>
        /// </summary>
        public int? CaCoinAmount { get; set; }
        /// <summary>
        ///  0 = inactive
        ///  1 = active (default)
        ///  2 = soft deleted
        /// </summary>
        public int Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<InternalTransaction> InternalTransactions { get; set; }
    }
}
