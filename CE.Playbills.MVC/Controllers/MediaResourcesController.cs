using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CE.Playbills.MVC.Models;
using System.Net.Http;
using System.Net;
using CE.ArchiveWebSites.Core.Helpers;
using CE.ArchiveWebSites.Core.Models;
using Newtonsoft.Json;

namespace CE.Playbills.MVC.Controllers
{
    public class MediaRecordsController : Controller
    {
        public async Task<IActionResult> SearchResults(string apiUrl, int pageNumber = 1, int pageSize = 5)
        {
            if (apiUrl == null)
            {
                apiUrl = $"https://localhost:44300/api/v1/archives/10/mediarecords?pagesize={pageSize}&pagenumber={pageNumber}";
            }
            var responseObject = await HttpClientHelper.GetFromLMARApi<List<MediaRecord>>(apiUrl);

            List<MediaRecord> mediaRecords = responseObject.ResponseData;

            if (mediaRecords == null)
            {
                return NotFound();
            }

            foreach (var mr in mediaRecords)
            {
                mr.ImageLink = mr.Links.FirstOrDefault(l => l.Rel == "Thumbnail").Href;
            };

            PaginationDetails paginationDetails = new PaginationDetails();

            var paginationHeader = responseObject.ResponseHeaders.Where(a => a.Key == "X-Pagination")
                                        .FirstOrDefault().Value;
            if (paginationHeader != null)
            {
                paginationDetails = JsonConvert.DeserializeObject<PaginationDetails>(paginationHeader.FirstOrDefault());
            }
            PagedMediaRecords pagedMediaRecords = new PagedMediaRecords()
            {
                PaginationDetails = paginationDetails,
                MediaRecords = mediaRecords
            };

            return View(pagedMediaRecords);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responseObject = await HttpClientHelper.GetFromLMARApi<MediaRecord>($"https://localhost:44300/api/v1/archives/10/mediarecords/{id}");

            MediaRecord mediaRecord = responseObject.ResponseData;

            if (mediaRecord == null)
            {
                return NotFound();
            }

            mediaRecord.ImageLink = mediaRecord.Links.FirstOrDefault(l => l.Rel == "Thumbnail").Href;

            return View(mediaRecord);
        }
    }
}