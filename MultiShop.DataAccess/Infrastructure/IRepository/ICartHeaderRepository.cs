using MultiShop.Models.Models;
using MultiShop.Models.Request;
using MultiShop.Models.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiShop.DataAccess.Infrastructure.IRepository
{
    public interface ICartHeaderRepository
    {
        Task<IEnumerable<CartHeader>> GetAllCart();
        Task<CartHeader> GetCartByUserId(Guid UserId);
        Task<CartCreateResponse> CreateCart(CartCreateRequest cartHeader);
        Task<CartHeader> EditCart(CartHeader cartHeader);
        Task<bool> DeleteCart(Guid userId);
        bool IsCartExist(Guid userId);

    }
}
