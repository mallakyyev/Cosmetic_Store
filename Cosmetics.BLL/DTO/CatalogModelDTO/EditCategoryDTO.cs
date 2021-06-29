using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cosmetics.BLL.DTO.CatalogModelDTO
{
    public class EditCategoryDTO
    {
        public int Id { get; set; }

        public ICollection<CategoryTranslateDTO> CategoryTranslates { get; set; }

        [Required]
        public int DisplayOrder { get; set; }

        public int? ParentCategoryId { get; set; }

        [Required]
        public bool Published { get; set; }
    }
}
