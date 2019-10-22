using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CE.ArchiveWebSites.Core.Models;

namespace CE.ArchiveWebSites.Core.Areas.ECommerce.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ArchivesDbContext _archivesDbContext;
        private readonly ShoppingCart _shoppingCart;

        public OrderRepository(ArchivesDbContext archivesDbContext, ShoppingCart shoppingCart)
        {
            _archivesDbContext = archivesDbContext;
            _shoppingCart = shoppingCart;
        }

        public IEnumerable<OrderSize> GetSizes()
        {
            return _archivesDbContext.OrderSizes;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            var shoppingCartItems = _shoppingCart.ShoppingCartItems;
            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();

            order.OrderDetails = new List<OrderDetail>();
            //adding the order with its details

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingCartItem.Amount,
                    MediaResourceId = shoppingCartItem.MediaResourceId,
                    Size = shoppingCartItem.OrderSize,
                    SizeId = shoppingCartItem.SizeId,
                    Price = shoppingCartItem.Cost
                };

                order.OrderDetails.Add(orderDetail);
            }

            _archivesDbContext.Orders.Add(order);

            _archivesDbContext.SaveChanges();
        }
    }
}
