using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CE.ArchiveWebSites.Core.Models;

namespace CE.Leodis.MVC.Models
{
    public class MediaResource : BaseMediaResource
    {
        public string SomeLeodisProperty { get; set; }

        public string ImageLink { get; set; }

    }
}
