using Cosmetics.DAL.Models.Configuration;   
using Cosmetics.DAL.Models.Enums;
using Cosmetics.DAL.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmetics.DAL.Models.Cart
{
    public class CheckoutPickupPoint
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int? CountryId { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Comment { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public DateTime OrderCreateDate { get; set; }

        public string UserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public Country Country { get; set; }

        public ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
