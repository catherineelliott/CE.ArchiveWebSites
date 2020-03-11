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
    public class MediaResourcesController : Controller
    {
        public async Task<IActionResult> SearchResults(string apiUrl, int pageNumber = 1, int pageSize = 5)
        {
            if (apiUrl == null)
            {
                apiUrl = $"https://localhost:44300/api/v1/archives/10/mediarecords?pagesize={pageSize}&pagenumber={pageNumber}";
            }
            var responseObject = await HttpClientHelper.GetFromLMARApi<List<MediaResource>>(apiUrl);

            List<MediaResource> mediaResources = responseObject.ResponseData;

            if (mediaResources == null)
            {
                return NotFound();
            }

            foreach (var mr in mediaResources)
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
            PagedMediaResources pagedMediaResources = new PagedMediaResources()
            {
                PaginationDetails = paginationDetails,
                MediaResources = mediaResources
            };

            return View(pagedMediaResources);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responseObject = await HttpClientHelper.GetFromLMARApi<MediaResource>($"https://localhost:44300/api/v1/archives/10/mediarecords/{id}");

            MediaResource mediaResource = responseObject.ResponseData;

            if (mediaResource == null)
            {
                return NotFound();
            }

            mediaResource.ImageLink = mediaResource.Links.FirstOrDefault(l => l.Rel == "Thumbnail").Href;

            return View(mediaResource);
        }
    }
}