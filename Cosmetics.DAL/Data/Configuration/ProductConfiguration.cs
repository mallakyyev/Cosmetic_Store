using Cosmetics.DAL.Models.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmetics.DAL.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(p => p.ManufactureId).IsRequired(false);
            builder.Property(p => p.ShowOnHomePage).IsRequired();
            builder.Property(p => p.Published).IsRequired();
            builder.Property(p => p.ProductAsNew).IsRequired();
            builder.Property(p => p.NewStartDate).IsRequired(false);
            builder.Property(p => p.EndStartDate).IsRequired(false);
            builder.Property(p => p.QuantityInSet).IsRequired(false);
            builder.Property(p => p.ProductCode).IsRequired(false);
            builder.Property(p => p.PictureName).IsRequired(false);
            builder.Property(p => p.DeliveryRate).IsRequired();
            builder.HasOne(p => p.Manufacture).WithMany(p => p.Products).HasForeignKey(p => p.ManufactureId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
