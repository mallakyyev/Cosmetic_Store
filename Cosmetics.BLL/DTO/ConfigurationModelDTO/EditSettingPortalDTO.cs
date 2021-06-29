using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cosmetics.BLL.DTO.ConfigurationModelDTO
{
    public class EditSettingPortalDTO
    {
        public int Id { get; set; }
        public IFormFile FormFile { get; set; }

        public string LogoPortal { get; set; }

        public string AddressPortal { get; set; }

        public string PhonePortal { get; set; }

        public string EmailPortal { get; set; }

        public ICollection<SettingPortalTranslateDTO> SettingPortalTranslates { get; set; }
    }
}
