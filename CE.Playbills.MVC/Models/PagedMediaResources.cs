using CE.ArchiveWebSites.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CE.Playbills.MVC.Models
{
    public class PagedMediaResources
    {
        public PaginationDetails PaginationDetails { get; set; }

        public List<MediaResource> MediaResources { get; set; }
    }
}
