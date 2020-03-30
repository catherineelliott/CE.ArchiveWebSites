using CE.ArchiveWebSites.Core.Models;
using CE.Leodis.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CE.Leodis.MVC.ViewModels
{
    //Not using at the mo
    public class DetailsViewModel
    {
        public MediaRecord MediaRecord { get; set; }
        //public IEnumerable<MediaRecordComment> Comments { get; set; }
        //public string ImageLink { get; set; }

        public CheckoutDetails CheckoutDetails { get; set; }
    }
}
