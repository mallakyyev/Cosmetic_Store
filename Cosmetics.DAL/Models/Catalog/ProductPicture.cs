using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmetics.DAL.Models.Catalog
{
    public class ProductPicture
    {
        public int Id { get; set; }

        public string PictureName { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
