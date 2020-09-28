using Br.Com.LojaQueExplode.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Br.Com.LojaQueExplode.Infra.Mapping
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.ComplementaryProductDataID).IsRequired();

            builder.HasOne(x=>x.ComplementaryProductData)
                .WithOne(x=>x.Product)
                .HasForeignKey<Product>(x => x.ComplementaryProductDataID);

            builder.HasOne(x => x.Category)
                 .WithMany(x => x.Products)
                 .HasForeignKey(x => x.CategoryId);


            builder.HasIndex(x => x.Name).IsUnique();


        }
    }
}
