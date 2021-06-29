using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmetics.DAL.Models.Configuration
{
    public class SettingPortal
    {
        public int Id { get; set; }

        public string LogoPortal { get; set; }

        public string AddressPortal { get; set; }

        public string PhonePortal { get; set; }

        public string EmailPortal { get; set; }

        public ICollection<SettingPortalTranslate> SettingPortalTranslates { get; set; }
    }
}
