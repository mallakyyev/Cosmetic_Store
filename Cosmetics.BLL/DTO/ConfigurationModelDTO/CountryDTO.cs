using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cosmetics.BLL.DTO.ConfigurationModelDTO
{
    public class CountryDTO
    {
        public int Id { get; set; }

        /// <summary>
        /// Наименование страны
        /// </summary>
        [Required]
        public string NameCountry { get; set; }

        public bool Published { get; set; }

        /// <summary>
        /// Цена доставки товара в страну
        /// </summary>
        [Required]
        public decimal PriceDelivery { get; set; }
    }
}
