using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmetics.DAL.Models.Catalog
{
    public class Manufacture
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int DisplayOrder { get; set; }

        public string Description { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
