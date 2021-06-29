using Cosmetics.DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cosmetics.BLL.DTO.CartModelDTO
{
    public class CheckoutPickupPointCreateDTO
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }
        
        public int? CountryId { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public OrderStatus OrderStatus { get; set; }

        public DateTime OrderCreateDate { get; set; }

        public string Comment { get; set; }

        public string UserId { get; set; }

        public ICollection<ShoppingCartCreateDTO> ShoppingCarts { get; set; }
    }
}
