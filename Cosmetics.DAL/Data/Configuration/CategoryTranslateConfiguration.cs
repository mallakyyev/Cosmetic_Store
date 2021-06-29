using Cosmetics.DAL.Models.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmetics.DAL.Data.Configuration
{
    public class CategoryTranslateConfiguration : IEntityTypeConfiguration<CategoryTranslate>
    {
        public void Configure(EntityTypeBuilder<CategoryTranslate> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Description).IsRequired(false);
            builder.Property(p => p.CategoryId).IsRequired();
            builder.Property(p => p.LanguageCulture).IsRequired();
            builder.HasOne(p => p.Category).WithMany(p => p.CategoryTranslates).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Cascade);
            //builder.HasOne(p => p.Language).WithMany(p => p.CategoryTranslates).HasForeignKey(p => p.LanguageCulture).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
