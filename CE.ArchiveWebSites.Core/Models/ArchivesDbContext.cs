using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CE.ArchiveWebSites.Core.Areas.Commenting.Models;
using CE.ArchiveWebSites.Core.Areas.ECommerce.Models;
using CE.ArchiveWebSites.Core.Areas.WebCard.Models;
using CE.ArchiveWebSites.Core.Areas.ECommerce.Enums;

namespace CE.ArchiveWebSites.Core.Models
{
    public class ArchivesDbContext : DbContext
    {

        public ArchivesDbContext() {}

        public ArchivesDbContext(DbContextOptions<ArchivesDbContext> options) : base(options)
        {

        }

        public DbSet<MediaRecordComment> MediaRecordComments { get; set; }
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
            modelBuilder.Entity<MediaRecordComment>().HasData(new MediaRecordComment { MediaRecordCommentId = 1, Comment = "That's lovely", CreatedBy = "Catherine", MediaRecordId = 1 });
            modelBuilder.Entity<MediaRecordComment>().HasData(new MediaRecordComment { MediaRecordCommentId = 2, Comment = "Not great", CreatedBy = "Catherine", MediaRecordId = 2 });
            modelBuilder.Entity<MediaRecordComment>().HasData(new MediaRecordComment { MediaRecordCommentId = 3, Comment = "That's a lovely Playbill", CreatedBy = "Catherine", MediaRecordId = 9999 });
            modelBuilder.Entity<MediaRecordComment>().HasData(new MediaRecordComment { MediaRecordCommentId = 4, Comment = "Not a great Playbill", CreatedBy = "Catherine", MediaRecordId = 4 });

            //seed sizes
            modelBuilder.Entity<OrderSize>().HasData(new OrderSize { OrderSizeId = 1, Size = "10 x 8 inches" });
            modelBuilder.Entity<OrderSize>().HasData(new OrderSize { OrderSizeId = 2, Size = "12 x 9 inches" });
            modelBuilder.Entity<OrderSize>().HasData(new OrderSize { OrderSizeId = 3, Size = "16 x 12 inches" });
            //seed costs
            modelBuilder.Entity<OrderCost>().HasData(new OrderCost { OrderCostId = 1, SizeId = 1, PrintFinish = PrintFinish.Matt, Cost = Convert.ToDecimal(6.67) });
            modelBuilder.Entity<OrderCost>().HasData(new OrderCost { OrderCostId = 2, SizeId = 1, PrintFinish = PrintFinish.Gloss, Cost = Convert.ToDecimal(6.67) });
            modelBuilder.Entity<OrderCost>().HasData(new OrderCost { OrderCostId = 3, SizeId = 2, PrintFinish = PrintFinish.Matt, Cost = Convert.ToDecimal(9.17) });
            modelBuilder.Entity<OrderCost>().HasData(new OrderCost { OrderCostId = 4, SizeId = 2, PrintFinish = PrintFinish.Gloss, Cost = Convert.ToDecimal(9.17) });
            modelBuilder.Entity<OrderCost>().HasData(new OrderCost { OrderCostId = 5, SizeId = 3, PrintFinish = PrintFinish.Matt, Cost = Convert.ToDecimal(10.84) });
            modelBuilder.Entity<OrderCost>().HasData(new OrderCost { OrderCostId = 6, SizeId = 3, PrintFinish = PrintFinish.Gloss, Cost = Convert.ToDecimal(10.84) });
        }
    }
}
