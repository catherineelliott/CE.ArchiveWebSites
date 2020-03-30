using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CE.ArchiveWebSites.Core.Models;

namespace CE.ArchiveWebSites.Core.Areas.ECommerce.Models
{
    public class ShoppingCart
    {
        private readonly ArchivesDbContext _archivesDbContext;

        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        private ShoppingCart(ArchivesDbContext ArchivesDbContext)
        {
            _archivesDbContext = ArchivesDbContext;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var context = services.GetService<ArchivesDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };

        }

        public void AddToCart(int mediaRecordId, int sizeId)
        {
            var shoppingCartItem =
                    _archivesDbContext.ShoppingCartItems
                    .SingleOrDefault(
                        s => s.MediaRecordId == mediaRecordId 
                        && s.SizeId == sizeId
                        && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                var cost = _archivesDbContext.OrderCosts.FirstOrDefault(
                        c => c.SizeId == sizeId);
                var size = _archivesDbContext.OrderSizes.SingleOrDefault(
                        s => s.OrderSizeId == sizeId);

                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    MediaRecordId = mediaRecordId,
                    SizeId = sizeId,
                    OrderSize = size,
                    Cost = cost.Cost,
                    Amount = 1
                };

                _archivesDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _archivesDbContext.SaveChanges();
        }

        public int RemoveFromCart(int mediaRecordId)
        {
            var shoppingCartItem =
                    _archivesDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.MediaRecordId == mediaRecordId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _archivesDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _archivesDbContext.SaveChanges();

            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??
                   (ShoppingCartItems =
                       _archivesDbContext.ShoppingCartItems
                       .Where(c => c.ShoppingCartId == ShoppingCartId)
                       .Include(s => s.OrderSize)
                           .ToList());
        }

        public void ClearCart()
        {
            var cartItems = _archivesDbContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _archivesDbContext.ShoppingCartItems.RemoveRange(cartItems);

            _archivesDbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _archivesDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Cost * c.Amount).Sum();
            return total;
        }
    }
}
