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
        public async Task<IActionResult> SearchResults()
        {
            var mediaResources = await HttpClientHelper.GetFromLMARApi<List<MediaResource>>("https://localhost:44300/api/v1/archives/1/mediarecords?pagenumber=2");
            if (mediaResources == null)
            {
                return NotFound();
            }
            //Need to update CE API instead
            //var leodisMediaResources = mediaResources.Where(ai => ai.ArchiveId == 1);
            return View(mediaResources);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaResource = await HttpClientHelper.GetFromLMARApi<MediaResource>($"https://localhost:44300/api/v1/archives/1/mediarecords/{id}");

            if (mediaResource == null)
            {
                return NotFound();
            }

            CheckoutDetails checkoutDetails = new CheckoutDetails()
            {
                SelectedSizeId = "1",
                Sizes = _orderRepository.GetSizes()
            };

            DetailsViewModel detailsViewModel = new DetailsViewModel()
            {
                MediaResource = mediaResource,
                CheckoutDetails = checkoutDetails
            };

            return View(detailsViewModel);
        }
    }
}