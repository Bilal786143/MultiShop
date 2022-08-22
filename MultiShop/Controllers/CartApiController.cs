using Microsoft.AspNetCore.Mvc;
using MultiShop.DataAccess.Infrastructure.IRepository;
using MultiShop.Models.Models.DTOs;
using System;
using System.Threading.Tasks;

namespace MultiShop.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class CartApiController : BaseController
    {
        private readonly ICartRepository _cart;
        public CartApiController(ICartRepository cart)
        {
            _cart = cart;
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<CartDto>> GetCart(string userId)
        {
            try
            {
                return Ok(await _cart.GetCartByUserId(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse(ex));
            }
        }
        [HttpPost]
        public async Task<ActionResult<CartDto>> AddCart(CartDto cartDto)
        {
            try
            {
                return Ok(await _cart.CreateUpdateCart(cartDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse(ex));
            }
        }
        [HttpPost]
        public async Task<ActionResult<CartDto>> UpdateCart(CartDto cartDto)
        {
            try
            {
                return Ok(await _cart.CreateUpdateCart(cartDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse(ex));
            }
        }
        [HttpGet("{cartId}")]
        public async Task<ActionResult<CartDto>> RemoveCart(int cartId)
        {
            try
            {
                return Ok(await _cart.RemoveFromCart(cartId));
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse(ex));
            }
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<CartDto>> ClearCart(string userId)
        {
            try
            {
                return Ok(await _cart.ClearCart(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse(ex));
            }
        }
    }
}
