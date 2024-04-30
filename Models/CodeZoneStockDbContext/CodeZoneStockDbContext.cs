using CodeZoneStock.Models.DataEntities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace CodeZoneStock.Models.CodeZoneStockDbContext
{
    public class CodeZoneStockDbContext : DbContext
    {
        public CodeZoneStockDbContext(DbContextOptions<CodeZoneStockDbContext> options) : base(options)
        {
        }

        public DbSet<Store> Stores { get; set; }
        public DbSet<Item> Items { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Store>()
            //    .HasMany(s => s.Items)
            //    .WithMany(i => i.Stores)
            //    .UsingEntity<ItemStore>(
            //        j => j
            //            .HasOne(s => s.Item)
            //            .WithMany(t => t.ItemStores)
            //            .HasForeignKey(s => s.ItemsId),
            //        j => j
            //            .HasOne(s => s.Store)
            //            .WithMany(t => t.ItemStores)
            //            .HasForeignKey(s => s.StoresId),
            //        j => j
            //        .HasKey(k => new { k.StoresId, k.ItemsId })
            //    );

            modelBuilder.Entity<StoreItem>()
                .HasKey(k => new { k.StoreId, k.ItemId });

            modelBuilder.Entity<StoreItem>()
                .HasOne(i => i.Item)
                .WithMany(i => i.ItemStores)
                .HasForeignKey(i => i.ItemId);

            modelBuilder.Entity<StoreItem>()
                .HasOne(s => s.Store)
                .WithMany(s => s.ItemStores)
                .HasForeignKey(s => s.StoreId);
        }
    }
}
