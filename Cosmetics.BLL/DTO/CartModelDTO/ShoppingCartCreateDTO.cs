using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cosmetics.BLL.DTO.CartModelDTO
{
    public class ShoppingCartCreateDTO
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public int CheckoutPickupPointId { get; set; }

        [Required]
        public int DeliveryRate { get; set; }

        [Required]
        public int DeliveryRateTotal { get; set; }
    }
}
