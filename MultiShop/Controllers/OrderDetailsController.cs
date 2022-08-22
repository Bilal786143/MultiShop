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
    public class OrderDetailsController : BaseController
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
            catch (Exception ex)
            {
               return BadRequest(ErrorResponse(ex));
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
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse(ex));

            }
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderDetailsById(int id )
        {
            try
            {
                return Ok( await _orderDetails.GetOrderDetailsById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse(ex));
            }
        }
    }
}
