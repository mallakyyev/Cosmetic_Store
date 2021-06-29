using Cosmetics.BLL.DTO.CatalogModelDTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cosmetics.BLL.DTO.ProductModelDTO
{
    public class CreateProductDTO
    {
        public ICollection<ProductTranslateDTO> ProductTranslates { get; set; }

        public int[] CategoriesId { get; set; }

        /// <summary>
        /// Код продукта
        /// </summary>
        [Required]
        public string ProductCode { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        [Required]
        public int ManufactureId { get; set; }

        [Required]
        public bool ShowOnHomePage { get; set; }

        [Required]
        public bool Published { get; set; }

        [Required]
        public bool ProductAsNew { get; set; }

        public DateTime? NewStartDate { get; set; }

        public DateTime? EndStartDate { get; set; }

        /// <summary>
        /// Количество в наборе
        /// </summary>
        [Required]
        public int QuantityInSet { get; set; }

        /// <summary>
        /// Коэфициэнт достаки
        /// </summary>
        public int DeliveryRate { get; set; }

        public IFormFile FormFile { get; set; }

        public IEnumerable<IFormFile> FormFiles { get; set; }

    }
}
