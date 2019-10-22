using CE.ArchiveWebSites.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CE.ArchiveWebSites.Core.Areas.ECommerce.Models
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }
        public int MediaResourceId { get; set; }
        public int Amount { get; set; }
        public int SizeId { get; set; }
        public OrderSize OrderSize { get; set; }
        public bool Sepia { get; set; }
        public PrintFinish Finish { get; set; }
        public decimal Cost { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
