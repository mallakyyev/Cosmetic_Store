using Cosmetics.DAL.Models.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmetics.DAL.Data.Configuration
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.CurrencyCode).IsRequired();
            builder.Property(p => p.Rate).IsRequired();
            builder.Property(p => p.DisplayOrder).IsRequired();
            builder.Property(p => p.Published).IsRequired();
        }
    }
}
