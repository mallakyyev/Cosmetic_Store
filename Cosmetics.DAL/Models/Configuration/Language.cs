using Cosmetics.DAL.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmetics.DAL.Models.Configuration
{
    public class Language
    {

        public string Culture { get; set; }

        public string Name { get; set; }

        public string LanguageCode { get; set; }        

        public string FlagImage { get; set; }

        public bool Published { get; set; }

        public int? DisplayOrder { get; set; }

        //public ICollection<CategoryTranslate> CategoryTranslates { get; set; }

        //public ICollection<ProductTranslate> ProductTranslates { get; set; }

        //public ICollection<SettingPortalTranslate> SettingPortalTranslates { get; set; }
    }
}
