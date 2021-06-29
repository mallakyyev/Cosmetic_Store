using Cosmetics.DAL.Models.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmetics.DAL.Data.Configuration
{
    public class SettingPortalTranslateConfiguration : IEntityTypeConfiguration<SettingPortalTranslate>
    {
        public void Configure(EntityTypeBuilder<SettingPortalTranslate> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.SettingPortalId).IsRequired();
            builder.Property(p => p.LanguageCulture).IsRequired();
            builder.Property(p => p.AboutPortal).IsRequired();
            builder.Property(p => p.DeliveryPortal).IsRequired();
            builder.HasOne(p => p.SettingPortal).WithMany(p => p.SettingPortalTranslates).HasForeignKey(p => p.SettingPortalId).OnDelete(DeleteBehavior.Cascade);
            //builder.HasOne(p => p.Language).WithMany(p => p.SettingPortalTranslates).HasForeignKey(p => p.LanguageCulture).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
