using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmetics.BLL.DTO.CatalogModelDTO
{
    public class FilterCategoryDTO
    {
        public string LanguageCulture { get; set; }

        public int? ParentId { get; set; }
    }
}
