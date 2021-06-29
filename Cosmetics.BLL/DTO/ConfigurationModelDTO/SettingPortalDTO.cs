using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cosmetics.BLL.DTO.ConfigurationModelDTO
{
    public class SettingPortalDTO
    {
        public int Id { get; set; }

        [Required]
        public string LogoPortal { get; set; }

        public string AddressPortal { get; set; }

        public string PhonePortal { get; set; }

        public string EmailPortal { get; set; }

        public string AboutPortal { get; set; }

        public string DeliveryPortal { get; set; }
    }
}
