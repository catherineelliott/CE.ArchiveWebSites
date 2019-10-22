using CE.ArchiveWebSites.Core.Areas.ECommerce.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CE.ArchiveWebSites.Core
{
    //May not need
    public static class IServiceCollectionExtension
    {
        //Not using.  Kept for reference
        public static IServiceCollection AddArchiveWebSitesCoreLibrary(this IServiceCollection services)
        {
            services.AddScoped<ShoppingCart>(sp => ShoppingCart.GetCart(sp));
            return services;
        }
    }
}
