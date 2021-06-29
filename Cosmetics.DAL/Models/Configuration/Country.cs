using Cosmetics.DAL.Models.Cart;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cosmetics.DAL.Models.Configuration
{
    public class Country
    {
        public int Id { get; set; }

        /// <summary>
        /// Наименование страны
        /// </summary>
        public string NameCountry { get; set; }

        public bool Published { get; set; }

        /// <summary>
        /// Цена доставки товара в страну
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceDelivery { get; set; }

        public ICollection<CheckoutPickupPoint> CheckoutPickupPoints { get; set; }
    }
}
