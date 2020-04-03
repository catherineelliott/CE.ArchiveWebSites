using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CE.Leodis.MVC.Models;
using System.Net.Http;
using System.Net;
using CE.ArchiveWebSites.Core.Helpers;
using CE.Leodis.MVC.ViewModels;
using CE.ArchiveWebSites.Core.Areas.ECommerce.Models;
using CE.ArchiveWebSites.Core.Areas.Commenting.Models;
using CE.ArchiveWebSites.Core.Models;
using Newtonsoft.Json;

namespace CE.Leodis.MVC.Controllers
{
    public class MediaRecordController : Controller
    {
        private readonly IMediaRecordCommentsRepository _mediaRecordCommentsRepository;
        private readonly IOrderRepository _orderRepository;

        public MediaRecordController(IMediaRecordCommentsRepository mediaRecordCommentsRepository,
                                            IOrderRepository orderRepository)
        {
            _mediaRecordCommentsRepository = mediaRecordCommentsRepository;
            _orderRepository = orderRepository;
        }
        public async Task<IActionResult> SearchResults(string apiUrl, int pageNumber = 1, int pageSize = 5)
        {
            if (apiUrl == null)
            {
                apiUrl = $"https://localhost:44300/api/v1/archives/9/mediarecords?pagesize={pageSize}&pagenumber={pageNumber}";
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

            var responseObject = await HttpClientHelper.GetFromLMARApi<MediaRecord>($"https://localhost:44300/api/v1/archives/9/mediarecords/{id}");

            MediaRecord mediaRecord = responseObject.ResponseData;

            if (mediaRecord == null)
            {
                return NotFound();
            }

            CheckoutDetails checkoutDetails = new CheckoutDetails()
            {
                SelectedSizeId = "1",
                Sizes = _orderRepository.GetSizes()
            };

            mediaRecord.ImageLink = mediaRecord.Links.FirstOrDefault(l => l.Rel == "Thumbnail").Href;

            DetailsViewModel detailsViewModel = new DetailsViewModel()
            {
                MediaRecord = mediaRecord,
                CheckoutDetails = checkoutDetails
            };

            return View(detailsViewModel);
        }
    }
}