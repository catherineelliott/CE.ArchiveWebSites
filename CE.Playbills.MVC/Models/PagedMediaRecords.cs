using CE.ArchiveWebSites.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CE.Playbills.MVC.Models
{
    public class PagedMediaRecords
    {
        public PaginationDetails PaginationDetails { get; set; }

        public List<MediaRecord> MediaRecords { get; set; }
    }
}
