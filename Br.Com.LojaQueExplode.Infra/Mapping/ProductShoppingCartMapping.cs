using Br.Com.LojaQueExplode.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Br.Com.LojaQueExplode.Infra.Mapping
{
    public class ProductShoppingCartMapping : IEntityTypeConfiguration<ProductShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ProductShoppingCart> builder)
        {
            builder.HasKey(x => new { x.ProductId, x.ShoppingCartId });

            builder.HasOne<Product>(x => x.Product)
                .WithMany(x => x.ProductShoppingCarts)
                .HasForeignKey(x => x.ProductId);

            builder.HasOne<ShoppingCart>(x => x.ShoppingCart)
                .WithMany(x => x.ProductShoppingCarts)
                .HasForeignKey(x => x.ShoppingCartId);


        }
    }
}
