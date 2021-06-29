using Cosmetics.DAL.Models.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmetics.DAL.Data.Configuration
{
    public class CategoryAndProductConfiguration : IEntityTypeConfiguration<CategoryAndProduct>
    {
        public void Configure(EntityTypeBuilder<CategoryAndProduct> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.CategoryId).IsRequired();
            builder.Property(p => p.ProductId).IsRequired();
            builder.HasOne(p => p.Category).WithMany(p => p.CategoryAndProducts).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Product).WithMany(p => p.CategoryAndProducts).HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
