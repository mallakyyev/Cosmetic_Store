using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cosmetics.BLL.DTO.ManufactureModelDTO
{
    public class CreateManufactureDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int DisplayOrder { get; set; }

        public string Description { get; set; }
    }
}
