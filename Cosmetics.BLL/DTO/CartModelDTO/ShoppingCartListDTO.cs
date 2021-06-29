using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmetics.BLL.DTO.CartModelDTO
{
    public class ShoppingCartListDTO
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public int CheckoutPickupPointId { get; set; }

        public int DeliveryRate { get; set; }

        public int DeliveryRateTotal { get; set; }
    }
}
