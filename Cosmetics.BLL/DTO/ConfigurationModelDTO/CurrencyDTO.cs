using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cosmetics.BLL.DTO.ConfigurationModelDTO
{
    public class CurrencyDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Курс
        /// </summary>
        [Required]
        public double Rate { get; set; }

        public int DisplayOrder { get; set; }

        [Required]
        public bool Published { get; set; }
    }
}
