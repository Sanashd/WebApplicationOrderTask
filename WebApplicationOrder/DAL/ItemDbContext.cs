using Microsoft.EntityFrameworkCore;
using WebApplicationOrder.Models.DBEntities;

namespace WebApplicationOrder.DAL
{
    public class ItemDbContext : DbContext
    {
        public ItemDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderMaster> OrderMasters { get; set; }

    }
}
