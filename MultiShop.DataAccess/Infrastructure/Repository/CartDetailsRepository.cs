using Microsoft.EntityFrameworkCore;
using MultiShop.DataAccess.Data;
using MultiShop.DataAccess.Infrastructure.IRepository;
using MultiShop.Models.Models;
using MultiShop.Models.Request;
using MultiShop.Models.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.DataAccess.Infrastructure.Repository
{
    public class CartDetailsRepository : ICartDetailsRepository
    {
        private readonly ApplicationDbContext _context;
        public CartDetailsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CartDetailsCreateResponse> CreateCartDetails(CartDetailsCreateRequest cartDetails)
        {
            var createCartDetails = new CartDetails
            {
                CartHeaderFId = cartDetails.CartHeaderFId,
                ProductFId = cartDetails.ProductFId,
                ProductQuantity = cartDetails.ProductQuantity,
                TotalPrice = cartDetails.TotalPrice
            };
            var result = await _context.CartDetails.AddAsync(createCartDetails);
            await _context.SaveChangesAsync();
            return new CartDetailsCreateResponse
            {
                //CartDetailsCreateResponse
                CartDetails = result.Entity
            };
        }

        public async Task<bool> DeleteCartDetails(int id)
        {
            var result = await _context.CartDetails.FirstOrDefaultAsync(x => x.Id == id);
            _context.CartDetails.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CartDetailsEditResponse> EditCartDetails(CartDetailsEditRequest cartDetails)
        {
            var editCartDetails = new CartDetails
            {
                Id = cartDetails.Id,
                CartHeaderFId = cartDetails.CartHeaderFId,
                ProductFId = cartDetails.ProductFId,
                ProductQuantity = cartDetails.ProductQuantity,
                TotalPrice = cartDetails.TotalPrice
            };
            _context.CartDetails.Update(editCartDetails);
            await _context.SaveChangesAsync();
            return new CartDetailsEditResponse
            {
                CartDetails = editCartDetails
            };
        }

        public async Task<IEnumerable<CartDetails>> GetAllCartDetails()
        {
            return await _context.CartDetails.ToListAsync();
        }

        public async Task<CartDetails> GetCartDetailsById(int id)
        {
            var result = await _context.CartDetails.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public bool IsCartDetailsExist(int id)
        {
            return _context.CartDetails.Any(x => x.Id == id);
        }
    }
}
