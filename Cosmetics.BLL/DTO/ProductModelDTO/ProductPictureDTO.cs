using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cosmetics.BLL.DTO.ProductModelDTO
{
    public class ProductPictureDTO
    {
        public int Id { get; set; }

        [Required]
        public string PictureName { get; set; }

        [Required]
        public int ProductId { get; set; }

        public ProductDTO Product { get; set; }
    }
}
