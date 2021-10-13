﻿// <auto-generated />
using System;
using Br.Com.LojaQueExplode.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Br.Com.LojaQueExplode.Infra.Migrations
{
    [DbContext(typeof(LojaQueExplodeContext))]
    [Migration("20211010151723_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Br.Com.LojaQueExplode.Domain.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Br.Com.LojaQueExplode.Domain.Entities.ComplementaryProductData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("WarrantyTime")
                        .HasColumnType("integer");

                    b.Property<int>("Weight")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("ComplementaryProductDatas");
                });

            modelBuilder.Entity("Br.Com.LojaQueExplode.Domain.Entities.Permission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Permissions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2462edf3-7af3-4c31-864a-a52bb282b834"),
                            Name = "Common"
                        },
                        new
                        {
                            Id = new Guid("2753eb8a-1789-4b7c-ad60-0b9138326bd8"),
                            Name = "Administration"
                        });
                });

            modelBuilder.Entity("Br.Com.LojaQueExplode.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ComplementaryProductDataID")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ComplementaryProductDataID")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Br.Com.LojaQueExplode.Domain.Entities.ProductPhoto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("MimiType")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductPhotos");
                });

            modelBuilder.Entity("Br.Com.LojaQueExplode.Domain.Entities.ProductShoppingCart", b =>
                {
                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ShoppingCartId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("ProductId", "ShoppingCartId");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("ProductShoppingCarts");
                });

            modelBuilder.Entity("Br.Com.LojaQueExplode.Domain.Entities.PurchaseStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Code")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.HasKey("Id");

                    b.ToTable("PurchaseStatus");

                    b.HasData(
                        new
                        {
                            Id = new Guid("bb78e11b-3ef7-446d-8a96-aa919961660e"),
                            Code = 1,
                            Description = "",
                            Name = "Open"
                        },
                        new
                        {
                            Id = new Guid("c2626930-649d-4dc3-b462-7336a9553531"),
                            Code = 2,
                            Description = "",
                            Name = "RequestedProducts"
                        },
                        new
                        {
                            Id = new Guid("acfcc32e-9bde-41fc-870a-7d4bbf278a72"),
                            Code = 3,
                            Description = "",
                            Name = "PaymentMade"
                        },
                        new
                        {
                            Id = new Guid("84878113-d4f2-4f22-bfdd-0643249e11e6"),
                            Code = 4,
                            Description = "",
                            Name = "PaymentApproved"
                        },
                        new
                        {
                            Id = new Guid("72c24800-10d5-4927-b967-10e03ade4ca4"),
                            Code = 5,
                            Description = "",
                            Name = "SendedProducts"
                        },
                        new
                        {
                            Id = new Guid("faba4ae0-c7fe-4582-9c07-af8f63cda12f"),
                            Code = 6,
                            Description = "",
                            Name = "PurchaseFinished"
                        });
                });

            modelBuilder.Entity("Br.Com.LojaQueExplode.Domain.Entities.ShoppingCart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("EndedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("PurchaseStatusId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PurchaseStatusId");

                    b.HasIndex("UserId");

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("Br.Com.LojaQueExplode.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("PermissionId")
                        .HasColumnType("uuid");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("PermissionId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Br.Com.LojaQueExplode.Domain.Entities.Product", b =>
                {
                    b.HasOne("Br.Com.LojaQueExplode.Domain.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Br.Com.LojaQueExplode.Domain.Entities.ComplementaryProductData", "ComplementaryProductData")
                        .WithOne("Product")
                        .HasForeignKey("Br.Com.LojaQueExplode.Domain.Entities.Product", "ComplementaryProductDataID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("ComplementaryProductData");
                });

            modelBuilder.Entity("Br.Com.LojaQueExplode.Domain.Entities.ProductPhoto", b =>
                {
                    b.HasOne("Br.Com.LojaQueExplode.Domain.Entities.Product", "Product")
                        .WithMany("Photos")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Br.Com.LojaQueExplode.Domain.Entities.ProductShoppingCart", b =>
                {
                    b.HasOne("Br.Com.LojaQueExplode.Domain.Entities.Product", "Product")
                        .WithMany("ProductShoppingCarts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Br.Com.LojaQueExplode.Domain.Entities.ShoppingCart", "ShoppingCart")
                        .WithMany("ProductShoppingCarts")
                        .HasForeignKey("ShoppingCartId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("ShoppingCart");
                });

            modelBuilder.Entity("Br.Com.LojaQueExplode.Domain.Entities.ShoppingCart", b =>
                {
                    b.HasOne("Br.Com.LojaQueExplode.Domain.Entities.PurchaseStatus", "PurchaseStatus")
                        .WithMany("ShoppingCarts")
                        .HasForeignKey("PurchaseStatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Br.Com.LojaQueExplode.Domain.Entities.User", "User")
                        .WithMany("ShoppingCarts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("PurchaseStatus");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Br.Com.LojaQueExplode.Domain.Entities.User", b =>
                {
                    b.HasOne("Br.Com.LojaQueExplode.Domain.Entities.Permission", "Permission")
                        .WithMany("Users")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Permission");
                });

            modelBuilder.Entity("Br.Com.LojaQueExplode.Domain.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Br.Com.LojaQueExplode.Domain.Entities.ComplementaryProductData", b =>
                {
                    b.Navigation("Product");
                });

            modelBuilder.Entity("Br.Com.LojaQueExplode.Domain.Entities.Permission", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Br.Com.LojaQueExplode.Domain.Entities.Product", b =>
                {
                    b.Navigation("Photos");

                    b.Navigation("ProductShoppingCarts");
                });

            modelBuilder.Entity("Br.Com.LojaQueExplode.Domain.Entities.PurchaseStatus", b =>
                {
                    b.Navigation("ShoppingCarts");
                });

            modelBuilder.Entity("Br.Com.LojaQueExplode.Domain.Entities.ShoppingCart", b =>
                {
                    b.Navigation("ProductShoppingCarts");
                });

            modelBuilder.Entity("Br.Com.LojaQueExplode.Domain.Entities.User", b =>
                {
                    b.Navigation("ShoppingCarts");
                });
#pragma warning restore 612, 618
        }
    }
}