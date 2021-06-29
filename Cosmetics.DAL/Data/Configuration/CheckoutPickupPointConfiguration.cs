using Cosmetics.DAL.Models.Cart;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmetics.DAL.Data.Configuration
{
    public class CheckoutPickupPointConfiguration : IEntityTypeConfiguration<CheckoutPickupPoint>
    {
        public void Configure(EntityTypeBuilder<CheckoutPickupPoint> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.FullName).IsRequired();
            builder.Property(p => p.Email).IsRequired();
            builder.Property(p => p.Phone).IsRequired();
            builder.Property(p => p.CountryId).IsRequired(false);
            builder.Property(p => p.City).IsRequired();
            builder.Property(p => p.Address).IsRequired();
            builder.Property(p => p.OrderStatus).IsRequired();
            builder.Property(p => p.OrderCreateDate).IsRequired();
            builder.Property(p => p.Comment).IsRequired(false);
            builder.Property(p => p.UserId).IsRequired(false);
            builder.HasOne(p => p.Country).WithMany(p => p.CheckoutPickupPoints).HasForeignKey(p => p.CountryId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
