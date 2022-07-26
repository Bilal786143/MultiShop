using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DataAccess.Infrastructure.IRepository;
using MultiShop.Models.Request;
using MultiShop.Models.Models;
using System;
using System.Threading.Tasks;

namespace MultiShop.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderApiController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        public OrderApiController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                return Ok(await _orderRepository.GetAllOrders());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error\nError While Retreiving Orders From Server");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            try
            {
                bool isExist = _orderRepository.IsOrderExist(id);
                if (!isExist)
                {
                    return NotFound();
                }
                return Ok(await _orderRepository.GetOrderById(id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderCreateRequest order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var createOrder = await _orderRepository.CreateOrder(order);
                    return Ok(createOrder);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error While Creating New Order");
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditOrder(OrderEditRequest order)
        {
            try
            {
                bool result = _orderRepository.IsOrderExist(order.Id);
                if (!result)
                {
                    return NotFound($"Order With Requesting Details Like ID : {order.Id} not found");
                }
                return Ok(await _orderRepository.EditOrder(order));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error While Updating Order at Current ID{order.Id}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteOrderById(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool result = _orderRepository.IsOrderExist(id);
                    if (!result)
                    {
                        return NotFound($"Order With Requesting Details (Order Id: {id}) to Delete Is not found");
                    }
                    await _orderRepository.DeleteOrder(id);
                    return Ok($"Order With ID : {id} is Delete Successfully");
                }
                return NotFound($"Please Double Check the Requesting ID {id} to be delete.\nModel State is InValid");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Deleting Order");
            }
        }

    }
}
