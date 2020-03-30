using CE.ArchiveWebSites.Core.Areas.Commenting.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CE.ArchiveWebSites.Core.Areas.Commenting.Models
{
    public class MediaRecordComment
    {
        public int MediaRecordCommentId { get; set; }       
        public string Comment { get; set; }
        public string CreatedBy { get; set; }
        public int MediaRecordId { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
    }
}
