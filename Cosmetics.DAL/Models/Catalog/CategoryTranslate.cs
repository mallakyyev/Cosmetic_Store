using Cosmetics.DAL.Models.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmetics.DAL.Models.Catalog
{
    public class CategoryTranslate
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public string LanguageCulture { get; set; }

        public Category Category { get; set; }

        //public Language Language { get; set; }
    }
}
