using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cosmetics.BLL.DTO.CatalogModelDTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public int DisplayOrder { get; set; }

        [Required]
        public bool Published { get; set; }

        public int? ParentCategoryId { get; set; }

        //public CategoryDTO ParentCategory { get; set; }

        //public ICollection<CategoryDTO> Categories { get; set; }

        //public ICollection<CategoryAndProductDTO> CategoryAndProducts { get; set; }

        //public ICollection<CategoryTranslateDTO> CategoryTranslates { get; set; }
    }
}
