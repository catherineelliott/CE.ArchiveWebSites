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
using Microsoft.AspNetCore.Http;

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

        public async Task<IActionResult> SearchResults(string apiUrl, int pageNumber = 1, int pageSize = 5 )
        {
            //TODO Use common methods
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

        public async Task<IActionResult> LatestImages(string apiUrl, int pageNumber = 1, int pageSize = 5)
        {
            ApiResponse<List<MediaRecord>> mediaRecords = await GetMediaRecords(apiUrl, pageNumber, pageSize);

            if (mediaRecords.ResponseData == null)
            {
                return NotFound();
            }

            var pagedMediaRecords = GetPagedMediaRecords(mediaRecords);

            return View(pagedMediaRecords);
        }

        
        public async Task<ApiResponse<List<MediaRecord>>> GetMediaRecords(string apiUrl, int pageNumber, int pageSize)
        {
            //Need to add sort to API for latest images
            if (apiUrl == null)
            {
                apiUrl = $"https://localhost:44300/api/v1/archives/9/mediarecords?pagesize={pageSize}&pagenumber={pageNumber}";
            }
            var responseObject = await HttpClientHelper.GetFromLMARApi<List<MediaRecord>>(apiUrl);

            return responseObject;
        }

        public PagedMediaRecords GetPagedMediaRecords(ApiResponse<List<MediaRecord>> mediaRecords)
        {

            foreach (var mr in mediaRecords.ResponseData)
            {
                mr.ImageLink = mr.Links.FirstOrDefault(l => l.Rel == "Thumbnail").Href;
            };

            PaginationDetails paginationDetails = new PaginationDetails();

            var paginationHeader = mediaRecords.ResponseHeaders.Where(a => a.Key == "X-Pagination")
                                        .FirstOrDefault().Value;
            if (paginationHeader != null)
            {
                paginationDetails = JsonConvert.DeserializeObject<PaginationDetails>(paginationHeader.FirstOrDefault());
            }
            PagedMediaRecords pagedMediaRecords = new PagedMediaRecords()
            {
                PaginationDetails = paginationDetails,
                MediaRecords = mediaRecords.ResponseData
            };

            return pagedMediaRecords;
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