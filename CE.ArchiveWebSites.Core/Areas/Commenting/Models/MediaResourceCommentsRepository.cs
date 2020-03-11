using CE.ArchiveWebSites.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CE.ArchiveWebSites.Core.Areas.Commenting.Models
{
    public class MediaResourceCommentsRepository : IMediaResourceCommentsRepository
    {
        private readonly ArchivesDbContext _archivesDbContext;

        public MediaResourceCommentsRepository(ArchivesDbContext archiveDbContext)
        {
            _archivesDbContext = archiveDbContext;
        }
        //Maybe don't need.  CommentsListViewComponent can get from database.  This doesn't add anything.
        public IEnumerable<MediaResourceComment> AllCommentsByMediaResourceId(int? mediaResourceId)
        {
            return _archivesDbContext.MediaResourceComments.Where(c => c.MediaResourceId == mediaResourceId);
        }
    }
}
