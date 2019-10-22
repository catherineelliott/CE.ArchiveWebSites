using CE.ArchiveWebSites.Core.Enums;
using CE.ArchiveWebSites.Core.Areas.ECommerce.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CE.Leodis.MVC.Models
{
    public class CheckoutDetails
    {
        [Required]
        [Display(Name = "Size")]
        public string SelectedSizeId { get; set; }
        public IEnumerable<OrderSize> Sizes { get; set; }
        public PrintFinish Finish { get; set; }
        public bool Sepia { get; set; }
    }
}
