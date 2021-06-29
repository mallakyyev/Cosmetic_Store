using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cosmetics.BLL.DTO.ConfigurationModelDTO
{
    public class SettingPortalTranslateDTO
    {
        public int Id { get; set; }

        [Required]
        public int SettingPortalId { get; set; }
        
        [Required]
        public string LanguageCulture { get; set; }

        [Required]
        public string AboutPortal { get; set; }

        [Required]
        public string DeliveryPortal { get; set; }
    }
}
