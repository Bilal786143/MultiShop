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
    public class OrderApiController : BaseController
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
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse(ex));
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
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse(ex));
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
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse(ex));
            }
        }

        [HttpPut]
        public async Task<ActionResult> EditOrder(OrderEditRequest order)
        {
            try
            {
                bool result = _orderRepository.IsOrderExist(order.Id);
                if (!result)
                {
                    return NotFound();
                }
                return Ok(await _orderRepository.EditOrder(order));
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse(ex));
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
                        return NotFound();
                    }
                  return Ok(  await _orderRepository.DeleteOrder(id));
                   
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse(ex));
            }
        }

    }
}
