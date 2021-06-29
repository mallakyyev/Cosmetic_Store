using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmetics.DAL.Models.Configuration
{
    public class SettingPortalTranslate
    {
        public int Id { get; set; }

        public int SettingPortalId { get; set; }

        public string LanguageCulture { get; set; }

        public string AboutPortal { get; set; }

        public string DeliveryPortal { get; set; }

        public SettingPortal SettingPortal { get; set; }

        public Language Language { get; set; }
    }
}
