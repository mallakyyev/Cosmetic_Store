using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cosmetics.BLL.DTO.CartModelDTO;
using Microsoft.AspNetCore.Mvc;

namespace Cosmetics.Web.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DeliveryMethod()
        {
            return View();
        }

        public IActionResult OrderSuccessful()
        {
            return View();
        }

        //[HttpGet]
        //public IActionResult DeliveryMethod(CheckoutPickupPointCreateDTO model)
        //{
        //    return View(model);
        //}
    }
}