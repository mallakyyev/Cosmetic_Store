using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cosmetics.BLL.DTO.ConfigurationModelDTO
{
    public class CreateLanguageDTO
    {
        [Required]
        public string Culture { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LanguageCode { get; set; }

        public string FlagImage { get; set; }

        public int? DisplayOrder { get; set; }

        [Required]
        public bool Published { get; set; }
    }
}
