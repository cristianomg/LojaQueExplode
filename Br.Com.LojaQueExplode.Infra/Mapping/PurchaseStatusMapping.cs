using Br.Com.LojaQueExplode.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Br.Com.LojaQueExplode.Infra.Mapping
{
    public class PurchaseStatusMapping : IEntityTypeConfiguration<PurchaseStatus>
    {
        public void Configure(EntityTypeBuilder<PurchaseStatus> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(40);
            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.Description).IsRequired().HasMaxLength(255);


            builder.HasMany(x => x.ShoppingCarts)
                .WithOne(x => x.PurchaseStatus)
                .HasForeignKey(x => x.PurchaseStatusId);
        }
    }
}
