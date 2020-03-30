using CE.ArchiveWebSites.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CE.ArchiveWebSites.Core.Areas.Commenting.Models
{
    public class MediaRecordCommentsRepository : IMediaRecordCommentsRepository
    {
        private readonly ArchivesDbContext _archivesDbContext;

        public MediaRecordCommentsRepository(ArchivesDbContext archiveDbContext)
        {
            _archivesDbContext = archiveDbContext;
        }
        //Maybe don't need.  CommentsListViewComponent can get from database.  This doesn't add anything.
        public IEnumerable<MediaRecordComment> AllCommentsByMediaRecordId(int? mediaRecordId)
        {
            return _archivesDbContext.MediaRecordComments.Where(c => c.MediaRecordId == mediaRecordId);
        }
    }
}
