﻿// <auto-generated />
using System;
using Clean.Api.Data.Access;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Clean.Api.DataAccess.Migrations
{
    [DbContext(typeof(CleanDbContext))]
    [Migration("20210325022436_ItemUpdates")]
    partial class ItemUpdates
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Clean.Api.DataAccess.Models.Items.Branch", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("BranchCode");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("BranchName");

                    b.HasKey("Code");

                    b.ToTable("Branch");
                });

            modelBuilder.Entity("Clean.Api.DataAccess.Models.Items.Brand", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("BrandCode");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("BrandName");

                    b.HasKey("Code");

                    b.ToTable("Brand");
                });

            modelBuilder.Entity("Clean.Api.DataAccess.Models.Items.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ItemId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BrandCode")
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("BrandCode");

                    b.Property<string>("Code")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("ItemCode");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("ItemDescription");

                    b.Property<string>("DiscountGroup")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("ItemDiscountGroup");

                    b.Property<string>("Errors")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ItemErrors");

                    b.Property<string>("FullCode")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ItemFullCode");

                    b.Property<string>("FullDescription")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ItemFullDescription");

                    b.Property<string>("FullType")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("ItemFullType");

                    b.Property<string>("OldCode")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ItemOldCode");

                    b.Property<string>("PriceListGroup")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("ItemPriceListGroup");

                    b.Property<int>("PurchaseQty")
                        .HasColumnType("int")
                        .HasColumnName("ItemPurchaseQty");

                    b.Property<string>("StockGroup")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("ItemStockGroup");

                    b.Property<string>("SupplierCode")
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("SupplierCode");

                    b.HasKey("Id");

                    b.HasIndex("BrandCode");

                    b.HasIndex("SupplierCode");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Clean.Api.DataAccess.Models.Items.ItemPrice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ItemPriceId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2")
                        .HasColumnName("ItemPriceDate");

                    b.Property<string>("ItemCode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("ItemCode");

                    b.Property<int>("ItemId")
                        .HasColumnType("int")
                        .HasColumnName("ItemId");

                    b.Property<bool>("PriceIncludesGST")
                        .HasColumnType("bit")
                        .HasColumnName("ItemPriceIncludesGST");

                    b.Property<int>("PriceListId")
                        .HasColumnType("int")
                        .HasColumnName("PriceListId");

                    b.Property<double>("UnitCost")
                        .HasColumnType("float")
                        .HasColumnName("ItemPriceUnitCost");

                    b.Property<double>("UnitPrice")
                        .HasColumnType("float")
                        .HasColumnName("ItemPriceUnitPrice");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("PriceListId");

                    b.ToTable("ItemPrice");
                });

            modelBuilder.Entity("Clean.Api.DataAccess.Models.Items.ItemStock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ItemStockId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bin")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("ItemStockBin");

                    b.Property<string>("BranchCode")
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("BranchCode");

                    b.Property<int>("Current")
                        .HasColumnType("int")
                        .HasColumnName("ItemStockCurrent");

                    b.Property<int>("ImportNumber")
                        .HasColumnType("int")
                        .HasColumnName("ItemStockImportNumber");

                    b.Property<string>("ItemCode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("ItemCode");

                    b.Property<int>("ItemId")
                        .HasColumnType("int")
                        .HasColumnName("ItemId");

                    b.Property<DateTime>("LastOrdered")
                        .HasColumnType("datetime2")
                        .HasColumnName("ItemStockLastOrdered");

                    b.Property<int>("Max")
                        .HasColumnType("int")
                        .HasColumnName("ItemStockMax");

                    b.Property<int>("Min")
                        .HasColumnType("int")
                        .HasColumnName("ItemStockMin");

                    b.Property<bool>("Reprocess")
                        .HasColumnType("bit")
                        .HasColumnName("ItemStockReprocess");

                    b.HasKey("Id");

                    b.HasIndex("BranchCode");

                    b.HasIndex("ItemId");

                    b.ToTable("ItemStocks");
                });

            modelBuilder.Entity("Clean.Api.DataAccess.Models.Items.PriceList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PriceListId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BrandCode")
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("BrandCode");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2")
                        .HasColumnName("PriceListDate");

                    b.HasKey("Id");

                    b.HasIndex("BrandCode");

                    b.ToTable("PriceLists");
                });

            modelBuilder.Entity("Clean.Api.DataAccess.Models.Items.Supplier", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("SupplierCode");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("SupplierName");

                    b.HasKey("Code");

                    b.ToTable("Supplier");
                });

            modelBuilder.Entity("Clean.Api.DataAccess.Models.Users.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RoleId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Authority")
                        .HasColumnType("int")
                        .HasColumnName("RoleAuthority");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("RoleName");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Clean.Api.DataAccess.Models.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UserId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("UserFirstName");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("UserIsDeleted");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("UserLastName");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("UserPassword");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("UserUsername");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Clean.Api.DataAccess.Models.Users.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Clean.Api.DataAccess.Models.Items.Item", b =>
                {
                    b.HasOne("Clean.Api.DataAccess.Models.Items.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandCode");

                    b.HasOne("Clean.Api.DataAccess.Models.Items.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierCode");

                    b.Navigation("Brand");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Clean.Api.DataAccess.Models.Items.ItemPrice", b =>
                {
                    b.HasOne("Clean.Api.DataAccess.Models.Items.Item", "Item")
                        .WithMany("Prices")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Clean.Api.DataAccess.Models.Items.PriceList", "PriceList")
                        .WithMany("Prices")
                        .HasForeignKey("PriceListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("PriceList");
                });

            modelBuilder.Entity("Clean.Api.DataAccess.Models.Items.ItemStock", b =>
                {
                    b.HasOne("Clean.Api.DataAccess.Models.Items.Branch", "Branch")
                        .WithMany()
                        .HasForeignKey("BranchCode");

                    b.HasOne("Clean.Api.DataAccess.Models.Items.Item", "Item")
                        .WithMany("Stock")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Clean.Api.DataAccess.Models.Items.PriceList", b =>
                {
                    b.HasOne("Clean.Api.DataAccess.Models.Items.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandCode");

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("Clean.Api.DataAccess.Models.Users.UserRole", b =>
                {
                    b.HasOne("Clean.Api.DataAccess.Models.Users.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Clean.Api.DataAccess.Models.Users.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Clean.Api.DataAccess.Models.Items.Item", b =>
                {
                    b.Navigation("Prices");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("Clean.Api.DataAccess.Models.Items.PriceList", b =>
                {
                    b.Navigation("Prices");
                });

            modelBuilder.Entity("Clean.Api.DataAccess.Models.Users.User", b =>
                {
                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
