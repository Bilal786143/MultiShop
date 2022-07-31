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
    public class CartDetailsApiController : ControllerBase
    {
        private readonly ICartDetailsRepository _cartDetails;
        public CartDetailsApiController(ICartDetailsRepository cartDetails)
        {
            _cartDetails = cartDetails;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                return Ok(await _cartDetails.GetAllCartDetails());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error\nError While Retreiving Cart Details From Server");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CartDetails>> GetCartDetailsById(int id)
        {
            try
            {
                bool isExist = _cartDetails.IsCartDetailsExist(id);
                if (!isExist)
                {
                    return NotFound();
                }
                return Ok(await _cartDetails.GetCartDetailsById(id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CartDetails>> CreateCartDetails(CartDetailsCreateRequest cartDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var createCartDetails = await _cartDetails.CreateCartDetails(cartDetails);
                    return Ok(createCartDetails);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error While Creating New Cart Details");
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditCartDetails(CartDetailsEditRequest cartDetails)
        {
            try
            {
                bool result = _cartDetails.IsCartDetailsExist(cartDetails.Id);
                if (!result)
                {
                    return NotFound($"Cart Details With Requesting Details Like ID : {cartDetails.Id} not found");
                }
                return Ok(await _cartDetails.EditCartDetails(cartDetails));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error While Updating Cart Details at Current ID{cartDetails.Id}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCartDetails(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool result = _cartDetails.IsCartDetailsExist(id);
                    if (!result)
                    {
                        return NotFound($"Cart Details With Requesting Details (Cart Details Id: {id}) to Delete Is not found");
                    }
                    await _cartDetails.DeleteCartDetails(id);
                    return Ok($"Cart Details With ID : {id} is Delete Successfully");
                }
                return NotFound($"Please Double Check the Requesting ID {id} to be delete.\nModel State is InValid");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Deleting Cart Details");
            }
        }

    }
}
