using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CE.ArchiveWebSites.Core.Areas.Commenting.Models;
using CE.ArchiveWebSites.Core.Areas.ECommerce.Models;
using CE.ArchiveWebSites.Core.Areas.WebCard.Models;

namespace CE.ArchiveWebSites.Core.Models
{
    public class ArchivesDbContext : DbContext
    {
        public ArchivesDbContext(DbContextOptions<ArchivesDbContext> options) : base(options)
        {

        }

        public DbSet<MediaResourceComment> MediaResourceComments { get; set; }
        public DbSet<WebCard> WebCards { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderSize> OrderSizes { get; set; }
        public DbSet<OrderCost> OrderCosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seed comments
            modelBuilder.Entity<MediaResourceComment>().HasData(new MediaResourceComment { MediaResourceCommentId = 1, Comment = "That's lovely", CreatedBy = "Catherine", MediaResourceId = 1 });
            modelBuilder.Entity<MediaResourceComment>().HasData(new MediaResourceComment { MediaResourceCommentId = 2, Comment = "Not great", CreatedBy = "Catherine", MediaResourceId = 2 });
            modelBuilder.Entity<MediaResourceComment>().HasData(new MediaResourceComment { MediaResourceCommentId = 3, Comment = "That's a lovely Playbill", CreatedBy = "Catherine", MediaResourceId = 9999 });
            modelBuilder.Entity<MediaResourceComment>().HasData(new MediaResourceComment { MediaResourceCommentId = 4, Comment = "Not a great Playbill", CreatedBy = "Catherine", MediaResourceId = 4 });

            //seed sizes
            modelBuilder.Entity<OrderSize>().HasData(new OrderSize { OrderSizeId = 1, Size = "10 x 8 inches" });
            modelBuilder.Entity<OrderSize>().HasData(new OrderSize { OrderSizeId = 2, Size = "12 x 9 inches" });
            modelBuilder.Entity<OrderSize>().HasData(new OrderSize { OrderSizeId = 3, Size = "16 x 12 inches" });
            //seed costs
            modelBuilder.Entity<OrderCost>().HasData(new OrderCost { OrderCostId = 1, SizeId = 1, PrintFinish = Enums.PrintFinish.Matt, Cost = Convert.ToDecimal(6.67) });
            modelBuilder.Entity<OrderCost>().HasData(new OrderCost { OrderCostId = 2, SizeId = 1, PrintFinish = Enums.PrintFinish.Gloss, Cost = Convert.ToDecimal(6.67) });
            modelBuilder.Entity<OrderCost>().HasData(new OrderCost { OrderCostId = 3, SizeId = 2, PrintFinish = Enums.PrintFinish.Matt, Cost = Convert.ToDecimal(9.17) });
            modelBuilder.Entity<OrderCost>().HasData(new OrderCost { OrderCostId = 4, SizeId = 2, PrintFinish = Enums.PrintFinish.Gloss, Cost = Convert.ToDecimal(9.17) });
            modelBuilder.Entity<OrderCost>().HasData(new OrderCost { OrderCostId = 5, SizeId = 3, PrintFinish = Enums.PrintFinish.Matt, Cost = Convert.ToDecimal(10.84) });
            modelBuilder.Entity<OrderCost>().HasData(new OrderCost { OrderCostId = 6, SizeId = 3, PrintFinish = Enums.PrintFinish.Gloss, Cost = Convert.ToDecimal(10.84) });
        }
    }
}
