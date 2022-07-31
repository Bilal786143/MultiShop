using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DataAccess.Infrastructure.IRepository;
using MultiShop.Models.Models;
using MultiShop.Models.Request;
using System;
using System.Threading.Tasks;

namespace MultiShop.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartHeaderApiController : ControllerBase
    {
        private readonly ICartHeaderRepository _cartHeader;
        public CartHeaderApiController(ICartHeaderRepository cartHeader)
        {
            _cartHeader = cartHeader;
        }
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                return Ok(await _cartHeader.GetAllCart());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error\nError While Retreiving Cart Header From Server");
            }
        }

        [HttpGet("{userId:Guid}")]
        public async Task<ActionResult<CartHeader>> GetCartByUserId(Guid userId)
        {
            try
            {
                bool isExist = _cartHeader.IsCartExist(userId);
                if (!isExist)
                {
                    return NotFound();
                }
                return Ok(await _cartHeader.GetCartByUserId(userId));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CartHeader>> CreateCart(CartCreateRequest cartHeader)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var createCart = await _cartHeader.CreateCart(cartHeader);
                    return Ok(createCart);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error While Creating New Cart Header");
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditCart(CartHeader cartHeader)
        {
            try
            {
                bool result = _cartHeader.IsCartExist(cartHeader.UserId);
                if (!result)
                {
                    return NotFound($"Cart Details With Requesting Details Like ID : {cartHeader.UserId} not found");
                }
                return Ok(await _cartHeader.EditCart(cartHeader));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error While Updating Cart Header at Current ID{cartHeader.UserId}");
            }
        }

        [HttpDelete("{userId:Guid}")]
        public async Task<ActionResult> DeleteOrderById(Guid userId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool result = _cartHeader.IsCartExist(userId);
                    if (!result)
                    {
                        return NotFound($"Cart With Requesting Details (Cart Header User Id: {userId}) to Delete Is not found");
                    }
                    await _cartHeader.DeleteCart(userId);
                    return Ok($"Cart With User ID : {userId} is Delete Successfully");
                }
                return NotFound($"Please Double Check the Requesting ID {userId} to be delete.\nModel State is InValid");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Deleting Cart Header");
            }
        }
    }
}
