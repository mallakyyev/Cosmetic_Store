using Cosmetics.DAL.Models.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmetics.DAL.Models.Catalog
{
    public class ProductTranslate
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string FullDescription { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public string MetaTitle { get; set; }

        public string LanguageCulture { get; set; }

        public int ProductId { get; set; }

        //public Language Language { get; set; }

        public Product Product { get; set; }
    }
}
