using Br.Com.LojaQueExplode.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Br.Com.LojaQueExplode.Infra.Mapping
{
    public class ComplementaryProductDataMapping : IEntityTypeConfiguration<ComplementaryProductData>
    {
        public void Configure(EntityTypeBuilder<ComplementaryProductData> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.WarrantyTime).IsRequired();
            builder.Property(x => x.Weight).IsRequired();

            builder.HasOne(x => x.Product)
                .WithOne(x => x.ComplementaryProductData)
                .HasForeignKey<Product>(x => x.ComplementaryProductDataID);
        }

    }
}
