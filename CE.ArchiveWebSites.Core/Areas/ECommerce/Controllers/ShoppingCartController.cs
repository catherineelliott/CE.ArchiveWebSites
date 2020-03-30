using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CE.ArchiveWebSites.Core.Areas.ECommerce.Models;
using CE.ArchiveWebSites.Core.Areas.ECommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CE.ArchiveWebSites.Core.Areas.ECommerce.Controllers
{
    [Area("ECommerce")]
    [Route("ECommerce/[controller]/[action]")]
    public class ShoppingCartController : Controller
    {
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }
        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(shoppingCartViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int mediaRecordId, int sizeId)
        {
            _shoppingCart.AddToCart(mediaRecordId, sizeId);

            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int mediaRecordId)
        {
            _shoppingCart.RemoveFromCart(mediaRecordId);

            return RedirectToAction("Index");
        }
    }
}
