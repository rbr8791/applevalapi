﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using applevalApi.Persistence;

namespace applevalapi.Persistence.Migrations
{
    [DbContext(typeof(ApplDbContext))]
    [Migration("20190722211650_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846");

            modelBuilder.Entity("applevalApi.Entities.ActiveToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("CurrentToken");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("SourceIdentifier");

                    b.Property<bool>("Status");

                    b.Property<DateTime>("TokenDate");

                    b.Property<DateTime>("TokenExpireDate");

                    b.Property<string>("UserName");

                    b.Property<string>("createdBy");

                    b.HasKey("Id");

                    b.ToTable("ActiveTokens");
                });

            modelBuilder.Entity("applevalApi.Entities.AuditLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Description");

                    b.Property<string>("EventType");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("RawParams");

                    b.Property<string>("UserAudited");

                    b.Property<string>("createdBy");

                    b.HasKey("Id");

                    b.ToTable("AuditLogs");
                });

            modelBuilder.Entity("applevalApi.Entities.AuditLogPurchase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<int?>("CustomerId");

                    b.Property<string>("Description");

                    b.Property<string>("EventType");

                    b.Property<DateTime>("Modified");

                    b.Property<DateTime?>("PurchaseDate");

                    b.Property<int?>("Quantity");

                    b.Property<string>("RawParams");

                    b.Property<string>("UserAudited");

                    b.Property<string>("createdBy");

                    b.Property<int?>("productId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("productId");

                    b.ToTable("AuditLogPurchases");
                });

            modelBuilder.Entity("applevalApi.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<bool>("Enabled");

                    b.Property<string>("ISOCountryCode");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("createdBy");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("applevalApi.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address1");

                    b.Property<string>("Address2");

                    b.Property<DateTime>("Created");

                    b.Property<bool>("Enabled");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("PaymentMethod");

                    b.Property<int>("countryId");

                    b.Property<string>("createdBy");

                    b.Property<int>("userId");

                    b.HasKey("Id");

                    b.HasIndex("countryId");

                    b.HasIndex("userId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("applevalApi.Entities.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<DateTime>("Modified");

                    b.Property<bool>("Status");

                    b.Property<string>("createdBy");

                    b.HasKey("Id");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("applevalApi.Entities.InvoiceDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Modified");

                    b.Property<float>("Price");

                    b.Property<int>("Quantity");

                    b.Property<string>("createdBy");

                    b.Property<int>("invoiceId");

                    b.Property<int>("productId");

                    b.HasKey("Id");

                    b.HasIndex("invoiceId");

                    b.HasIndex("productId");

                    b.ToTable("InvoiceDetails");
                });

            modelBuilder.Entity("applevalApi.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<DateTime>("Modified");

                    b.Property<string>("OrderNumber");

                    b.Property<bool>("Status");

                    b.Property<string>("createdBy");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("applevalApi.Entities.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Modified");

                    b.Property<int?>("OrderId");

                    b.Property<int>("Quantity");

                    b.Property<string>("createdBy");

                    b.Property<int>("productId");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("productId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("applevalApi.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description");

                    b.Property<int?>("Likes");

                    b.Property<DateTime>("Modified");

                    b.Property<float?>("Price");

                    b.Property<bool?>("Status");

                    b.Property<string>("createdBy");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("applevalApi.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("RoleName")
                        .IsRequired();

                    b.Property<bool>("Status");

                    b.Property<string>("createdBy");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("applevalApi.Entities.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<DateTime>("Modified");

                    b.Property<int>("Quantity");

                    b.Property<bool>("Status");

                    b.Property<string>("createdBy");

                    b.Property<int>("productId");

                    b.HasKey("Id");

                    b.HasIndex("productId");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("applevalApi.Entities.StockMovement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("DocumentID")
                        .IsRequired();

                    b.Property<DateTime>("Modified");

                    b.Property<int>("Quantity");

                    b.Property<int>("StockMovementTypeId");

                    b.Property<string>("createdBy");

                    b.Property<int>("productId");

                    b.HasKey("Id");

                    b.HasIndex("StockMovementTypeId");

                    b.HasIndex("productId");

                    b.ToTable("StockMovements");
                });

            modelBuilder.Entity("applevalApi.Entities.StockMovementType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("createdBy");

                    b.HasKey("Id");

                    b.ToTable("StockMovementTypes");
                });

            modelBuilder.Entity("applevalApi.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<bool>("Enabled");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<DateTime>("Modified");

                    b.Property<string>("Password");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<int>("RoleId");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.Property<string>("createdBy");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("applevalApi.Entities.AuditLogPurchase", b =>
                {
                    b.HasOne("applevalApi.Entities.User", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("applevalApi.Entities.Product", "product")
                        .WithMany()
                        .HasForeignKey("productId");
                });

            modelBuilder.Entity("applevalApi.Entities.Customer", b =>
                {
                    b.HasOne("applevalApi.Entities.Country", "country")
                        .WithMany()
                        .HasForeignKey("countryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("applevalApi.Entities.User", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("applevalApi.Entities.InvoiceDetail", b =>
                {
                    b.HasOne("applevalApi.Entities.Invoice", "invoice")
                        .WithMany("InvoiceDetails")
                        .HasForeignKey("invoiceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("applevalApi.Entities.Product", "product")
                        .WithMany()
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("applevalApi.Entities.OrderDetail", b =>
                {
                    b.HasOne("applevalApi.Entities.Order")
                        .WithMany("Details")
                        .HasForeignKey("OrderId");

                    b.HasOne("applevalApi.Entities.Product", "product")
                        .WithMany()
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("applevalApi.Entities.Stock", b =>
                {
                    b.HasOne("applevalApi.Entities.Product", "product")
                        .WithMany()
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("applevalApi.Entities.StockMovement", b =>
                {
                    b.HasOne("applevalApi.Entities.StockMovementType", "StockMovementType")
                        .WithMany()
                        .HasForeignKey("StockMovementTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("applevalApi.Entities.Product", "product")
                        .WithMany()
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("applevalApi.Entities.User", b =>
                {
                    b.HasOne("applevalApi.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}