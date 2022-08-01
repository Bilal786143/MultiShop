using Microsoft.EntityFrameworkCore;
using MultiShop.DataAccess.Data;
using MultiShop.DataAccess.Infrastructure.IRepository;
using MultiShop.DataAccess.Services;
using MultiShop.Models.Models;
using MultiShop.Models.Request;
using MultiShop.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.DataAccess.Infrastructure.Repository
{
    public class CartHeaderRepository : ICartHeaderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _service;
        public CartHeaderRepository(ApplicationDbContext context , IUserService service)
        {
            _context = context;
            _service = service;
        }
        public async Task<CartCreateResponse> CreateCart(CartCreateRequest cartHeader)
        {
            var cart = new CartHeader
            {
                UserId = _service.GetUserID(),
                //UserId = cartHeader.UserId,
                NoOfItems = cartHeader.NoOfItems
            };
            var result = await _context.CartHeader.AddAsync(cart);
            await _context.SaveChangesAsync();
            return new CartCreateResponse
            {
                //CartCreateResponse
                CartHeader = result.Entity
            };
        }

        public async Task<bool> DeleteCart(int id)
        {
            var result = await _context.CartHeader.FirstOrDefaultAsync(x => x.Id==id);
            _context.CartHeader.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CartHeader> EditCart(CartHeader cartHeader)
        {
            _context.CartHeader.Update(cartHeader);
            await _context.SaveChangesAsync();
            return cartHeader;
        }

        public async Task<IEnumerable<Models.Models.CartHeader>> GetAllCart()
        {
            return await _context.CartHeader.ToListAsync();
        }

        public async Task<CartHeader> GetCartByUserId(int id)
        {
            var result = await _context.CartHeader.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public bool IsCartExist(int id)
        {
            return _context.CartHeader.Any(x => x.Id ==id);
        }
    }
}
