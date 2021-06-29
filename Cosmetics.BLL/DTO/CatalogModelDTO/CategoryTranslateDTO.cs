using Cosmetics.BLL.DTO.ConfigurationModelDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cosmetics.BLL.DTO.CatalogModelDTO
{
    public class CategoryTranslateDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public int ParentId { get; set; }
        [Required]
        public string LanguageCulture { get; set; }

        //public CategoryDTO Category { get; set; }

        //public LanguageDTO Language { get; set; }
    }
}
