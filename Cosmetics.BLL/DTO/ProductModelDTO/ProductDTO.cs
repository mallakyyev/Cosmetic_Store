using Cosmetics.BLL.DTO.CatalogModelDTO;
using Cosmetics.BLL.DTO.ManufactureModelDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cosmetics.BLL.DTO.ProductModelDTO
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }
        
        public string FullDescription { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public string MetaTitle { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int? ManufactureId { get; set; }

        public string NameManufacture { get; set; }

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
        public int? QuantityInSet { get; set; }

        /// <summary>
        /// Код продукта
        /// </summary>
        [Required]
        public string ProductCode { get; set; }

        /// <summary>
        /// Коэфициэнт достаки
        /// </summary>
        public int DeliveryRate { get; set; }

        public string PictureName { get; set; }

        //public ManufactureDTO Manufacture { get; set; }

        //public ICollection<CategoryAndProductDTO> CategoryAndProducts { get; set; }

        //public ICollection<ProductPictureDTO> ProductPictures { get; set; }

        //public ICollection<ProductTranslateDTO> ProductTranslates { get; set; }
    }
}
