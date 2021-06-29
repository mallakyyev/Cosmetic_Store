using Cosmetics.DAL.Models.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmetics.DAL.Data.Configuration
{
    public class SettingPortalConfiguration : IEntityTypeConfiguration<SettingPortal>
    {
        public void Configure(EntityTypeBuilder<SettingPortal> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.LogoPortal).IsRequired();
            builder.Property(p => p.AddressPortal).IsRequired(false);
            builder.Property(p => p.PhonePortal).IsRequired(false);
            builder.Property(p => p.EmailPortal).IsRequired(false);
        }
    }
}
