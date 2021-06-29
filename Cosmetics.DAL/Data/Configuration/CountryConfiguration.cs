using Cosmetics.DAL.Models.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmetics.DAL.Data.Configuration
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.NameCountry).IsRequired();
            builder.Property(p => p.Published).IsRequired();
            builder.Property(p => p.PriceDelivery).HasColumnType("decimal(18,2)").IsRequired();
        }
    }
}
