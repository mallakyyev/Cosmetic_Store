using AutoMapper;
using Cosmetics.BLL.DTO.CartModelDTO;
using Cosmetics.DAL.Data;
using Cosmetics.DAL.Models.Cart;
using Cosmetics.DAL.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics.BLL.Services.Cart
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CartService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task CreateCheckoutPickupPoint(CheckoutPickupPointCreateDTO modelDTO, string userId)
        {
            var checkoutPickupPoint = _mapper.Map<CheckoutPickupPoint>(modelDTO);
            checkoutPickupPoint.OrderStatus = OrderStatus.Pending;
            checkoutPickupPoint.OrderCreateDate = DateTime.Now;
            if(userId != null)
            {
                checkoutPickupPoint.UserId = userId;
            }
            await _dbContext.CheckoutPickupPoints.AddAsync(checkoutPickupPoint);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditCheckoutPickupPoint(CheckoutPickupPointListDTO modelDTO)
        {
            CheckoutPickupPoint checkoutPickupPoint = _mapper.Map<CheckoutPickupPoint>(modelDTO);
            _dbContext.CheckoutPickupPoints.Update(checkoutPickupPoint);
            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<CheckoutPickupPointListDTO> GetAllCheckoutPickupPointList()
        {
            //var checkoutPickupPointListDTOs = _mapper.Map<IEnumerable<CheckoutPickupPoint>, IEnumerable<CheckoutPickupPointListDTO>>(GetList());
            return _mapper.ProjectTo<CheckoutPickupPointListDTO>(_dbContext.CheckoutPickupPoints.AsQueryable()); //GetList().ProjectTo<CheckoutPickupPointListDTO>();
            //return checkoutPickupPointListDTOs;
        }

        public IEnumerable<ShoppingCartListDTO> GetAllShoppingCartList()
        {
            //var shoppingCartListDTOs = _mapper.Map<IEnumerable<ShoppingCart>, IEnumerable<ShoppingCartListDTO>>(GetShoppingCartList());
            return _mapper.ProjectTo<ShoppingCartListDTO>(_dbContext.ShoppingCarts.AsQueryable());
        }

        public IEnumerable<ShoppingCartListDTO> GetAllShoppingCartListWhereCheckoutPickupPointId(int checkoutPickupPointId)
        {
            //var shoppingCartListDTOs = _mapper.Map<IEnumerable<ShoppingCart>, IEnumerable<ShoppingCartListDTO>>(GetShoppingCartList());
            return _mapper.ProjectTo<ShoppingCartListDTO>(_dbContext.ShoppingCarts.Where(p => p.CheckoutPickupPointId == checkoutPickupPointId).AsQueryable());
        }

        public CheckoutPickupPointListDTO GetCheckOrder(int id)
        {
            var checkoutPickupPointListDTO = _mapper.Map<CheckoutPickupPoint, CheckoutPickupPointListDTO>(_dbContext.CheckoutPickupPoints
                .Include(i => i.ShoppingCarts)
                .Include(i => i.Country)
                .SingleOrDefault(s => s.Id == id));
            return checkoutPickupPointListDTO;
        }

        public IEnumerable<CheckoutPickupPoint> GetList()
        {
            var checkoutPickupPoints = _dbContext.CheckoutPickupPoints;
            return checkoutPickupPoints;
        }

        public IEnumerable<ShoppingCart> GetShoppingCartList()
        {
            var shoppingCarts = _dbContext.ShoppingCarts;
            return shoppingCarts;
        }

        public IEnumerable<CheckoutPickupPointListDTO> GetUserAllOrders(string userId)
        {
            var checkoutPickupPoints = _dbContext.CheckoutPickupPoints.Where(p => p.UserId == userId).Include(i => i.ShoppingCarts);
            var orders = _mapper.Map<IEnumerable<CheckoutPickupPoint>, IEnumerable<CheckoutPickupPointListDTO>>(checkoutPickupPoints);
            return orders;
        }

        public async Task RemoveOrder(int id)
        {
            CheckoutPickupPoint checkoutPickup = await _dbContext.CheckoutPickupPoints.FindAsync(id);
            _dbContext.CheckoutPickupPoints.Remove(checkoutPickup);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveShoppingCart(int id)
        {
            ShoppingCart shoppingCart = await _dbContext.ShoppingCarts.FindAsync(id);
            _dbContext.ShoppingCarts.Remove(shoppingCart);
            await _dbContext.SaveChangesAsync();
        }
    }
}
