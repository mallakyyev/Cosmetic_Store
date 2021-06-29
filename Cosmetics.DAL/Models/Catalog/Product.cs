using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cosmetics.DAL.Models.Catalog
{
    public class Product
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int? ManufactureId { get; set; }

        public bool ShowOnHomePage { get; set; }

        public bool Published { get; set; }

        public bool ProductAsNew { get; set; }

        public DateTime? NewStartDate { get; set; }

        public DateTime? EndStartDate { get; set; }

        /// <summary>
        /// Количество в наборе
        /// </summary>
        public int? QuantityInSet { get; set; }

        /// <summary>
        /// Код продукта
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// Коэфициэнт достаки
        /// </summary>
        public int DeliveryRate { get; set; }

        public Manufacture Manufacture { get; set; }

        public string PictureName { get; set; }

        public ICollection<CategoryAndProduct> CategoryAndProducts { get; set; }

        public ICollection<ProductPicture> ProductPictures { get; set; }

        public ICollection<ProductTranslate> ProductTranslates { get; set; }
    }
}
