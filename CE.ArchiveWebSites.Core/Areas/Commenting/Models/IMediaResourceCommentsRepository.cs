using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CE.ArchiveWebSites.Core.Areas.Commenting.Models
{
    public interface IMediaResourceCommentsRepository
    {
        IEnumerable<MediaResourceComment> AllCommentsByMediaResourceId(int? mediaResourceId);
    }
}
