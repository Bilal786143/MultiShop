using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DataAccess.Infrastructure.IRepository;
using MultiShop.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailsRepository _orderDetails;

        public OrderDetailsController(IOrderDetailsRepository orderDetails)
        {
            _orderDetails = orderDetails;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                return Ok( await _orderDetails.GetAllOrdersDetails());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error\nError While Retreiving Orders From Server");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrderDetails(OrderDetailsRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _orderDetails.CreateOrderDetails(request));
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error\nError While Retreiving Orders From Server");
      
            }
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderDetailsById(int id )
        {
            try
            {
                return Ok( await _orderDetails.GetOrderDetailsById(id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error\nError While Retreiving Orders From Server");
            }
        }
    }
}
