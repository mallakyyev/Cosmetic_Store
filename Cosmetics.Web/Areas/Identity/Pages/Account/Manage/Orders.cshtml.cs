using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cosmetics.BLL.DTO.CartModelDTO;
using Cosmetics.BLL.Services.Cart;
using Cosmetics.DAL.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cosmetics.Web.Areas.Identity.Pages.Account.Manage
{
    public class OrdersModel : PageModel
    {
        private readonly ICartService _cartService;
        private readonly UserManager<ApplicationUser> _userManager;

        [BindProperty]
        public IEnumerable<CheckoutPickupPointListDTO> checkoutPickupPointListDTOs { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public OrdersModel(ICartService cartService, UserManager<ApplicationUser> userManager)
        {
            _cartService = cartService;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            checkoutPickupPointListDTOs = _cartService.GetUserAllOrders(user.Id);

            return Page();
        }
    }
}
