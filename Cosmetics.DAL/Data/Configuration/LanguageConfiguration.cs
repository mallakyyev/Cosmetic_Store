using Cosmetics.DAL.Models.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmetics.DAL.Data.Configuration
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.HasKey(p => p.Culture);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.LanguageCode).IsRequired();
            //builder.Property(p => p.Culture).IsRequired();
            builder.Property(p => p.FlagImage).IsRequired(false);
            builder.Property(p => p.Published).IsRequired();
            builder.Property(p => p.DisplayOrder).IsRequired(false);
        }
    }
}
