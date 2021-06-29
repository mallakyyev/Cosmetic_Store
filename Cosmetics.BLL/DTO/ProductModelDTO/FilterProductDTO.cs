using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmetics.BLL.DTO.ProductModelDTO
{
    public class FilterProductDTO
    {
        public string LanguageCulture { get; set; }

        public int? CategoryId { get; set; }

        public int page { get; set; } = 1;

        /// <summary>
        /// Количество элементов на странице
        /// </summary>
        public int pageSize { get; set; } = 30;
    }
}
