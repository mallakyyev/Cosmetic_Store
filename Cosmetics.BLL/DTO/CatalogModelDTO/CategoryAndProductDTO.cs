using Cosmetics.BLL.DTO.ProductModelDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cosmetics.BLL.DTO.CatalogModelDTO
{
    public class CategoryAndProductDTO
    {
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int ProductId { get; set; }

        public CategoryDTO Category { get; set; }

        public ProductDTO Product { get; set; }
    }
}
