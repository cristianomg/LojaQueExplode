using Br.Com.LojaQueExplode.Domain.Entities;
using Br.Com.LojaQueExplode.Domain.Enums;
using Br.Com.LojaQueExplode.Infra.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Br.Com.LojaQueExplode.Infra.Context
{
    public class LojaQueExplodeContext : DbContext
    {
        public LojaQueExplodeContext(DbContextOptions<LojaQueExplodeContext> options): base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
            this.Database.Migrate();
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ComplementaryProductData> ComplementaryProductDatas { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts{ get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPhoto> ProductPhotos { get; set; }
        public DbSet<ProductShoppingCart> ProductShoppingCarts { get; set; }
        public DbSet<PurchaseStatus> PurchaseStatus { get; set; }
        public DbSet<User> Users{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            modelBuilder.ApplyConfiguration(new CategoryMapping());
            modelBuilder.ApplyConfiguration(new ComplementaryProductDataMapping());
            modelBuilder.ApplyConfiguration(new PermissionMapping());
            modelBuilder.ApplyConfiguration(new ProductMapping());
            modelBuilder.ApplyConfiguration(new ProductPhotoMapping());
            modelBuilder.ApplyConfiguration(new ProductShoppingCartMapping());
            modelBuilder.ApplyConfiguration(new PurchaseStatusMapping());
            modelBuilder.ApplyConfiguration(new ShoppingCartMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Permission>().HasData(
                new Permission
                {
                    Id = Guid.NewGuid(),
                    Name = nameof(PermissionsEnum.Common)
                },
                new Permission {
                    Id = Guid.NewGuid(),
                    Name = nameof(PermissionsEnum.Administration)
                });
            modelBuilder.Entity<PurchaseStatus>().HasData(
                new PurchaseStatus
                {
                    Id = Guid.NewGuid(),
                    Name = nameof(PurchaseStatusEnum.Open),
                    Code = 1,
                    Description = ""
                },
                new PurchaseStatus
                {
                    Id = Guid.NewGuid(),
                    Name = nameof(PurchaseStatusEnum.RequestedProducts),
                    Code = 2,
                    Description = ""
                },
                new PurchaseStatus
                {
                    Id = Guid.NewGuid(),
                    Name = nameof(PurchaseStatusEnum.PaymentMade),
                    Code = 3,
                    Description = ""
                },
                new PurchaseStatus
                {
                    Id = Guid.NewGuid(),
                    Name = nameof(PurchaseStatusEnum.PaymentApproved),
                    Code = 4,
                    Description = ""
                },
                new PurchaseStatus
                {
                    Id = Guid.NewGuid(),
                    Name = nameof(PurchaseStatusEnum.SendedProducts),
                    Code = 5,
                    Description = "",
                },
                new PurchaseStatus
                {
                    Id = Guid.NewGuid(),
                    Name = nameof(PurchaseStatusEnum.PurchaseFinished),
                    Code = 6,
                    Description = ""
                }
                );
        }
    }
}
