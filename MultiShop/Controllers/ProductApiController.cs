using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DataAccess.Infrastructure.IRepository;
using MultiShop.Models.Request;
using MultiShop.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductRepository _products;

        public ProductApiController(IProductRepository products)
        {
            _products = products;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsList()
        {
            try
            {
                return Ok(await _products.GetProducts());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Content Not Found ");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductsById(int id)
        {
            try
            {
                bool isExist = _products.IsProductExist(id);
                if (!isExist)
                {
                    return BadRequest();
                }
                return Ok(await _products.GetProductById(id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server is not responding ");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProducts(ProductCreateRequest product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _products.CreateProduct(product));
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server is Not Rresponding");
            }
        }
        [HttpPut]
        public async Task<IActionResult> EditProducts(ProductEditRequest product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _products.EditProduct(product));
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server is not responding !!!");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProducts(int id)
        {
            try
            {
                bool isExist = _products.IsProductExist(id);
                if (!isExist)
                {
                    return NotFound();
                }
                await _products.DeleteProducts(id);
                return Ok($"Product With ID : {id} is Delete Successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server is not responding !!!");
            }
        }
    }
}
