﻿using MultiShop.Models.Models.DTOs;
using System.Threading.Tasks;

namespace MultiShop.DataAccess.Infrastructure.IRepository
{
   public interface ICartRepository
    {
        Task<CartDto> GetCartByUserId(string userId);
        Task<CartDto> CreateUpdateCart(CartDto cartDto);
        Task<bool> RemoveFromCart(int cartDetailsId);
        Task<bool> ClearCart(string userId);
    }
}
