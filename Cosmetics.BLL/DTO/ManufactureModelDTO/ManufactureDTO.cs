using Cosmetics.BLL.DTO.ProductModelDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cosmetics.BLL.DTO.ManufactureModelDTO
{
    public class ManufactureDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int DisplayOrder { get; set; }

        public string Description { get; set; }

        public ICollection<ProductDTO> Products { get; set; }
    }
}
