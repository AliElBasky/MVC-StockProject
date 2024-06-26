﻿// <auto-generated />
using CodeZoneStock.Models.CodeZoneStockDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CodeZoneStock.Migrations
{
    [DbContext(typeof(CodeZoneStockDbContext))]
    partial class CodeZoneStockDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CodeZoneStock.Models.DataEntities.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("CodeZoneStock.Models.DataEntities.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("CodeZoneStock.Models.DataEntities.StoreItem", b =>
                {
                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("StoreId", "ItemId");

                    b.HasIndex("ItemId");

                    b.ToTable("StoreItem");
                });

            modelBuilder.Entity("CodeZoneStock.Models.DataEntities.StoreItem", b =>
                {
                    b.HasOne("CodeZoneStock.Models.DataEntities.Item", "Item")
                        .WithMany("ItemStores")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CodeZoneStock.Models.DataEntities.Store", "Store")
                        .WithMany("ItemStores")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("CodeZoneStock.Models.DataEntities.Item", b =>
                {
                    b.Navigation("ItemStores");
                });

            modelBuilder.Entity("CodeZoneStock.Models.DataEntities.Store", b =>
                {
                    b.Navigation("ItemStores");
                });
#pragma warning restore 612, 618
        }
    }
}
