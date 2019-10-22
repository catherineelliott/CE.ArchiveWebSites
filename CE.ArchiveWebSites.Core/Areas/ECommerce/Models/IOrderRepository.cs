using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CE.ArchiveWebSites.Core.Areas.ECommerce.Models
{
    public interface IOrderRepository
    {
        IEnumerable<OrderSize> GetSizes();
        void CreateOrder(Order order);
    }
}
