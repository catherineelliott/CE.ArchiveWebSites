using CE.ArchiveWebSites.Core.Areas.ECommerce.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CE.ArchiveWebSites.Core.Areas.ECommerce.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int MediaRecordId { get; set; }
        public int Amount { get; set; }
        public int SizeId { get; set; }
        public OrderSize Size { get; set; }
        public bool Sepia { get; set; }
        public PrintFinish Finish { get; set; }
        public decimal Price { get; set; }
        public Order Order { get; set; }
    }
}
