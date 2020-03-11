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
    public class MediaResourcesController : Controller
    {
        private readonly IMediaResourceCommentsRepository _mediaResourceCommentsRepository;
        private readonly IOrderRepository _orderRepository;

        public MediaResourcesController(IMediaResourceCommentsRepository mediaResourceCommentsRepository,
                                            IOrderRepository orderRepository)
        {
            _mediaResourceCommentsRepository = mediaResourceCommentsRepository;
            _orderRepository = orderRepository;
        }
        public async Task<IActionResult> SearchResults(string apiUrl, int pageNumber = 1, int pageSize = 5)
        {
            if (apiUrl == null)
            {
                apiUrl = $"https://localhost:44300/api/v1/archives/9/mediarecords?pagesize={pageSize}&pagenumber={pageNumber}";
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

            var responseObject = await HttpClientHelper.GetFromLMARApi<MediaResource>($"https://localhost:44300/api/v1/archives/9/mediarecords/{id}");

            MediaResource mediaResource = responseObject.ResponseData;

            if (mediaResource == null)
            {
                return NotFound();
            }

            CheckoutDetails checkoutDetails = new CheckoutDetails()
            {
                SelectedSizeId = "1",
                Sizes = _orderRepository.GetSizes()
            };

            mediaResource.ImageLink = mediaResource.Links.FirstOrDefault(l => l.Rel == "Thumbnail").Href;

            DetailsViewModel detailsViewModel = new DetailsViewModel()
            {
                MediaResource = mediaResource,
                CheckoutDetails = checkoutDetails
            };

            return View(detailsViewModel);
        }
    }
}