using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DataAccess.Infrastructure.IRepository;
using MultiShop.Models.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartApiController : ControllerBase
    {
        private readonly ICartRepository _cart;
        protected ResponseDto _response;
        public CartApiController(ICartRepository cart )
        {
            _cart = cart;
            _response = new ResponseDto();
        }
        [HttpGet("Getcart/{userId}")]
        public  async Task <Object> GetCart(string userId)
        {
            try
            {
                var cart =  await _cart.GetCartByUserId(userId);
                _response.Result = cart;

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPost("AddCart")]
        public async Task<Object> AddCart(CartDto cartDto)
        {
            try
            {
                CartDto cart = await _cart.CreateUpdateCart(cartDto);
                _response.Result = cart;

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPost("UpdateCart")]
        public async Task<Object> UpdateCart(CartDto cartDto)
        {
            try
            {
                CartDto cart = await _cart.CreateUpdateCart(cartDto);
                _response.Result = cart;

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPost("RemoveCart")]
        public async Task<Object> RemoveCart( [FromBody]int cartId)
        {
            try
            {
                bool isSuccess = await _cart.RemoveFromCart(cartId);
                _response.Result = isSuccess;

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPost("ClearCart")]
        public async Task<Object> ClearCart([FromBody] string userId)
        {
            try
            {
                bool isSuccess = await _cart.ClearCart(userId);
                _response.Result = isSuccess;

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
