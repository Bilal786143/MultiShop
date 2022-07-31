using Microsoft.EntityFrameworkCore;
using MultiShop.DataAccess.Data;
using MultiShop.DataAccess.Infrastructure.IRepository;
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
        public CartHeaderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CartCreateResponse> CreateCart(CartCreateRequest cartHeader)
        {
            var cart = new CartHeader
            {
                UserId = cartHeader.UserId,
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

        public async Task<bool> DeleteCart(Guid userId)
        {
            var result = await _context.CartHeader.FirstOrDefaultAsync(x => x.UserId == userId);
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

        public async Task<CartHeader> GetCartByUserId(Guid userId)
        {
            var result = await _context.CartHeader.FirstOrDefaultAsync(x => x.UserId == userId);
            return result;
        }

        public bool IsCartExist(Guid userId)
        {
            return _context.CartHeader.Any(x => x.UserId == userId);
        }
    }
}
