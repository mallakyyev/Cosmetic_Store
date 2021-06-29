using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmetics.DAL.Models.Configuration
{
    /// <summary>
    /// Валюта
    /// </summary>
    public class Currency
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CurrencyCode { get; set; }

        /// <summary>
        /// Курс
        /// </summary>
        public double Rate { get; set; }

        public int DisplayOrder { get; set; }

        public bool Published { get; set; }
    }
}
