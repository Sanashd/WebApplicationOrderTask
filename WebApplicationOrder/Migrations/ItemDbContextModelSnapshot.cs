﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplicationOrder.DAL;

#nullable disable

namespace WebApplicationOrder.Migrations
{
    [DbContext(typeof(ItemDbContext))]
    partial class ItemDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApplicationOrder.Models.DBEntities.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemId"));

                    b.Property<decimal>("ItemCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ItemDesc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ItemId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("WebApplicationOrder.Models.DBEntities.OrderDetail", b =>
                {
                    b.Property<int>("OrderedDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderedDetailsId"));

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderedDetailsId");

                    b.HasIndex("ItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("WebApplicationOrder.Models.DBEntities.OrderMaster", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<decimal>("OrderedAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("OrderedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("OrderId");

                    b.ToTable("OrderMasters");
                });

            modelBuilder.Entity("WebApplicationOrder.Models.DBEntities.OrderDetail", b =>
                {
                    b.HasOne("WebApplicationOrder.Models.DBEntities.Item", "Item")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplicationOrder.Models.DBEntities.OrderMaster", "OrderMaster")
                        .WithMany("OrderDetail")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("OrderMaster");
                });

            modelBuilder.Entity("WebApplicationOrder.Models.DBEntities.Item", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("WebApplicationOrder.Models.DBEntities.OrderMaster", b =>
                {
                    b.Navigation("OrderDetail");
                });
#pragma warning restore 612, 618
        }
    }
}
