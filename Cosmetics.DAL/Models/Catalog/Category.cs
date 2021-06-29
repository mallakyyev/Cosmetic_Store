using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmetics.DAL.Models.Catalog
{
    public class Category
    {
        public int Id { get; set; }

        public int DisplayOrder { get; set; }

        public bool Published { get; set; }

        public int? ParentCategoryId { get; set; }

        public Category ParentCategory { get; set; }

        public ICollection<Category> Categories { get; set; }

        public ICollection<CategoryTranslate> CategoryTranslates { get; set; }

        public ICollection<CategoryAndProduct> CategoryAndProducts { get; set; }
    }
}
