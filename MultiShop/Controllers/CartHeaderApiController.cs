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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CartHeader>> GetCartById(int id)
        {
            try
            {
                bool isExist = _cartHeader.IsCartExist(id);
                if (!isExist)
                {
                    return NotFound();
                }
                return Ok(await _cartHeader.GetCartByUserId(id));
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

        [HttpPut]
        public async Task<ActionResult> EditCart(CartHeader cartHeader)
        {
            try
            {
                bool result = _cartHeader.IsCartExist(cartHeader.Id);
                if (!result)
                {
                    return NotFound($"Cart Header With Requesting Details Like ID : {cartHeader.Id} not found");
                }
                return Ok(await _cartHeader.EditCart(cartHeader));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error While Updating Cart Header at Current ID{cartHeader.Id}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteOrderById(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool result = _cartHeader.IsCartExist(id);
                    if (!result)
                    {
                        return NotFound($"Cart With Requesting Details (Cart Header User Id: {id}) to Delete Is not found");
                    }
                    await _cartHeader.DeleteCart(id);
                    return Ok($"Cart With ID : {id} is Delete Successfully");
                }
                return NotFound($"Please Double Check the Requesting ID {id} to be delete.\nModel State is InValid");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Deleting Cart Header");
            }
        }
    }
}
