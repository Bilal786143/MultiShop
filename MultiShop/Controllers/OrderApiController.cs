﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DataAccess.Infrastructure.IRepository;
using MultiShop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {
            try
            {
                if (order == null)
                {
                    return BadRequest();
                }

                var createOrder = await _orderRepository.CreateOrder(order);
                return CreatedAtAction(nameof(GetOrderById), new { id = createOrder.Id }, createOrder);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error While Creating New Order");
            }
        }


        [HttpPost]
        public async Task<ActionResult<Order>> EditOrder(Order order)
        {
            try
            {
                //if (id != category.Id)
                //{
                //    return BadRequest("Requewsting Category With id is Not Found So It's Can't be Updated");
                //}
                bool result = _orderRepository.IsOrderExist(order.Id);
                if (!result)
                {
                    return NotFound($"Order With Requesting Details Like ID : {order.Id} not found");
                }
                return await _orderRepository.EditOrder(order);
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
                bool result = _orderRepository.IsOrderExist(id);
                if (!result)
                {
                    return NotFound($"Order With Requesting Details (Order Id: {id}) to Delete Is not found");
                }
                await _orderRepository.DeleteOrder(id);
                return Ok($"Order With ID : {id} is Delete Successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Deleting Order");
            }
        }








    }
}
