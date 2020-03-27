using CE.ArchiveWebSites.Core.Areas.Commenting.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CE.ArchiveWebSites.Core.Areas.Commenting.Models
{
    public class MediaResourceComment
    {
        public int MediaResourceCommentId { get; set; }       
        public string Comment { get; set; }
        public string CreatedBy { get; set; }
        public int MediaResourceId { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
    }
}
