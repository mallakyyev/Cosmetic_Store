using Cosmetics.BLL.DTO.CartModelDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics.BLL.Services.Cart
{
    public interface ICartService
    {
        IEnumerable<CheckoutPickupPointListDTO> GetAllCheckoutPickupPointList();

        IEnumerable<ShoppingCartListDTO> GetAllShoppingCartListWhereCheckoutPickupPointId(int checkoutPickupPointId);

        Task CreateCheckoutPickupPoint(CheckoutPickupPointCreateDTO modelDTO,string userId);

        Task EditCheckoutPickupPoint(CheckoutPickupPointListDTO modelDTO);

        CheckoutPickupPointListDTO GetCheckOrder(int id);

        IEnumerable<ShoppingCartListDTO> GetAllShoppingCartList();

        IEnumerable<CheckoutPickupPointListDTO> GetUserAllOrders(string userId);
        
        Task RemoveOrder(int id);

        Task RemoveShoppingCart(int id);
    }
}
