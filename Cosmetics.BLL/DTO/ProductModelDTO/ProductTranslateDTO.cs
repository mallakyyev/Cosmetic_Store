using Cosmetics.BLL.DTO.ConfigurationModelDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cosmetics.BLL.DTO.ProductModelDTO
{
    public class ProductTranslateDTO
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        public string FullDescription { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public string MetaTitle { get; set; }

        [Required]
        public string LanguageCulture { get; set; }

        [Required]
        public int ProductId { get; set; }

        //public LanguageDTO Language { get; set; }

        //public ProductDTO Product { get; set; }
    }
}
