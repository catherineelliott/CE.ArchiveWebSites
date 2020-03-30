using CE.ArchiveWebSites.Core.Areas.Commenting.Models;
using CE.ArchiveWebSites.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CE.ArchiveWebSites.Core.Areas.Commenting.ViewComponents
{
    [ViewComponent(Name = "Core.CommentsList")]
    public class CommentsListViewComponent : ViewComponent
    {
        private readonly ArchivesDbContext db;

        public CommentsListViewComponent(ArchivesDbContext context)
        {
            db = context;
        }

        public IViewComponentResult Invoke(int mediaRecordId)
        {
            var items = GetItems(mediaRecordId);
            //View is in Views folder as Areas don't exist for View Components
            return View(items);
        }
        private IEnumerable<MediaRecordComment> GetItems(int mediaRecordId)
        {
            return db.MediaRecordComments.Where(x => x.MediaRecordId == mediaRecordId).ToList();
        }
    }
}
