using Br.Com.LojaQueExplode.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Br.Com.LojaQueExplode.Infra.Mapping
{
    public class PermissionMapping : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasMaxLength(30);


            builder.HasIndex(x => x.Name).IsUnique();


            builder.HasMany(x => x.Users)
                .WithOne(x => x.Permission)
                .HasForeignKey(x => x.PermissionId);

        }
    }
}
