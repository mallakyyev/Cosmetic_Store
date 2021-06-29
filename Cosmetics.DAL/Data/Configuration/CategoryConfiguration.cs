using Cosmetics.DAL.Models.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmetics.DAL.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.DisplayOrder).IsRequired();
            builder.Property(p => p.ParentCategoryId).IsRequired(false);
            builder.HasOne(p => p.ParentCategory).WithMany(p => p.Categories).HasForeignKey(p => p.ParentCategoryId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
