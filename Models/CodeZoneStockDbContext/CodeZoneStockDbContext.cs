using CodeZoneStock.Models.DataEntities;
using Microsoft.EntityFrameworkCore;

namespace CodeZoneStock.Models.CodeZoneStockDbContext
{
    public class CodeZoneStockDbContext : DbContext
    {
        public CodeZoneStockDbContext(DbContextOptions<CodeZoneStockDbContext> options) : base(options)
        {
        }

        public DbSet<Store> Stores { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
