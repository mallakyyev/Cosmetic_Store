using Cosmetics.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmetics.BLL.DTO.ProductModelDTO
{
    public class ProductViewModelDTO
    {
        public IEnumerable<ProductDTO> ListProductDTO { get; set; }

        public PageViewModelDTO PageViewModel { get; set; }
    }
}
