using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CE.ArchiveWebSites.Core.Helpers;
using CE.ArchiveWebSites.Core.Models;
using CE.Leodis.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace CE.Leodis.MVC.Controllers
{
    public class MediaRecordCollectionController : Controller
    {
        public async Task<IActionResult> AllGuidedTours()
        {
            var responseObject = await HttpClientHelper.GetFromLMARApi<List<MediaRecordCollection>>($"https://localhost:44300/api/v1/archives/1/mediarecordcollections?CollectionType=GuidedTour");

            List<MediaRecordCollection> mediaRecordCollections = responseObject.ResponseData;

            if (mediaRecordCollections == null)
            {
                return NotFound();
            }
            //Just get all media records for now so can display image
            foreach (var mrc in mediaRecordCollections )
            {
                var responseObjectMR = await HttpClientHelper.GetFromLMARApi<List<MediaRecord>>($"https://localhost:44300/api/v1/archives/9/mediarecords");
                List<MediaRecord> mediaRecords = responseObjectMR.ResponseData;
                foreach (var mr in mediaRecords)
                {
                    mr.ImageLink = mr.Links.FirstOrDefault(l => l.Rel == "Thumbnail").Href;
                }
                mrc.MediaRecords = mediaRecords;         
             }

            return View(mediaRecordCollections);
        }

        public async Task<IActionResult> GuidedTour(int? id, string collectionTitle, int displayOrder = 42)
        {
            ViewData["CollectionTitle"] = collectionTitle;

            //Just get media record for now
            //When set up with link between guided tours and media records will use mrc id and display order
            //Will need to sort by displayorder
            var responseObjectMR = await HttpClientHelper.GetFromLMARApi<MediaRecord>($"https://localhost:44300/api/v1/archives/9/mediarecords/{displayOrder}");
            MediaRecord mediaRecord = responseObjectMR.ResponseData;
            mediaRecord.ImageLink = mediaRecord.Links.FirstOrDefault(l => l.Rel == "Thumbnail").Href;

            return View(mediaRecord);
        }

    }
}