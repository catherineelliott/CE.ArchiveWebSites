using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CE.ArchiveWebSites.Core.Areas.ECommerce.Enums;

namespace CE.ArchiveWebSites.Core.Areas.ECommerce.Models
{
    public class OrderCost
    {
        public int OrderCostId { get; set; }
        public int SizeId { get; set; }
        public virtual OrderSize Size { get; set; }
        public decimal Cost { get; set; }
        public PrintFinish PrintFinish { get; set; }
    }
}
