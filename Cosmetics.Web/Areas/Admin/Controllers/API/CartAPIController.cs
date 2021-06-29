using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cosmetics.BLL.DTO.CartModelDTO;
using Cosmetics.BLL.Services.Cart;
using Cosmetics.DAL.Models.Users;
using Cosmetics.Web.Binder;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cosmetics.Web.Areas.Admin.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartAPIController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartAPIController(ICartService cartService, UserManager<ApplicationUser> userManager)
        {
            _cartService = cartService;
            _userManager = userManager;
        }

        // GET: api/CartAPI
        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<CheckoutPickupPointListDTO>(_cartService.GetAllCheckoutPickupPointList().AsQueryable(), loadOptions);
        }

        // GET: api/CartAPI/GetOrdersDetail?checkoutPickupPointId=1
        [HttpGet("GetOrdersDetail")]
        public object GetOrdersDetail(DataSourceLoadOptions loadOptions, int checkoutPickupPointId)
        {
            return DataSourceLoader.Load<ShoppingCartListDTO>(_cartService.GetAllShoppingCartListWhereCheckoutPickupPointId(checkoutPickupPointId), loadOptions);
        }

        // GET: api/CartAPI/GetCheckOrder?id=5
        [HttpGet("GetCheckOrder")]
        public CheckoutPickupPointListDTO GetCheckOrder(int id)
        {
            var check = _cartService.GetCheckOrder(id);
            return check;
        }

        // GET: api/Cart/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/CartAPI
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CheckoutPickupPointCreateDTO value)
        {
            if (ModelState.IsValid)
            {
                string userId = null;
                if (User.Identity.IsAuthenticated)
                {
                    userId = _userManager.GetUserId(User);
                }
                await _cartService.CreateCheckoutPickupPoint(value, userId);
                return Ok(value);
            }
            return BadRequest();
        }

        // PUT: api/CartAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CheckoutPickupPointListDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != value.Id)
            {
                return BadRequest();
            }

            await _cartService.EditCheckoutPickupPoint(value);

            return Ok(value);
        }

        // DELETE: api/CartAPI/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _cartService.RemoveOrder(id);
        }

        //DELETE: api/CartAPI/DeleteShoppingCart
        [HttpDelete("DeleteShoppingCart/{id}")]
        public async Task DeleteShoppingCart(int id)
        {
            await _cartService.RemoveShoppingCart(id);
        }
    }
}