using System;
using System.Collections.Generic;

namespace MobileShopAPI.Models
{
    public partial class ReportCategory
    {
        public ReportCategory()
        {
            Reports = new HashSet<Report>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
    }
}
