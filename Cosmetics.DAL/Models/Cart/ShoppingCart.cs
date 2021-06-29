using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cosmetics.DAL.Models.Cart
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        public int CheckoutPickupPointId { get; set; }

        public int DeliveryRate { get; set; }

        public int DeliveryRateTotal { get; set; }

        public CheckoutPickupPoint CheckoutPickupPoint { get; set; }
    }
}
