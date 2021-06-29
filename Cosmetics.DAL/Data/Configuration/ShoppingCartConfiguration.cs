using Cosmetics.DAL.Models.Cart;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmetics.DAL.Data.Configuration
{
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.ProductId).IsRequired();
            builder.Property(p => p.ProductName).IsRequired();
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(p => p.Quantity).IsRequired();
            builder.Property(p => p.TotalPrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(p => p.CheckoutPickupPointId).IsRequired();
            builder.Property(p => p.DeliveryRate).IsRequired();
            builder.Property(p => p.DeliveryRateTotal).IsRequired();
            builder.HasOne(p => p.CheckoutPickupPoint).WithMany(p => p.ShoppingCarts).HasForeignKey(p => p.CheckoutPickupPointId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
