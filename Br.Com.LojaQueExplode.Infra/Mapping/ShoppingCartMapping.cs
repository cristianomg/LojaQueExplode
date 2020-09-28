using Br.Com.LojaQueExplode.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Br.Com.LojaQueExplode.Infra.Mapping
{
    public class ShoppingCartMapping : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.User)
                .WithMany(x => x.ShoppingCarts)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.PurchaseStatus)
                .WithMany(x => x.ShoppingCarts)
                .HasForeignKey(x=>x.PurchaseStatusId);

            builder.HasMany(x => x.ProductShoppingCarts)
                .WithOne(x => x.ShoppingCart)
                .HasForeignKey(x => x.ShoppingCartId);

        }
    }
}
