using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DataAccess.Repository.IRepository;
using MultiShop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductRepository _products;

        public ProductApiController(IProductRepository products)
        {
            _products = products;
        }
        [HttpGet]
        public async Task <IActionResult> GetProductsList()
        {
            try
            {
               return Ok( await _products.GetProducts());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status404NotFound, "Content Not Found ");
            }
            
        }
        [HttpGet("{ id:int}")]
        public async Task<IActionResult> GetProductsById(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                else return Ok(await _products.GetProductById(id));

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Server is not responding ");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateProducts(Product product)
        {
            try
            {
                if (product == null)
                {
                    return NotFound();
                }
                else return
               Ok(await _products.CreateProduct(product));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Server is Not Rresponding");
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> EditProducts(Product product)
        {
            try
            {
                if (product == null)
                {
                    return NotFound();

                }
                else
                {
                    return Ok ( await _products.EditProduct(product));
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Server is not responding !!!");
            }
            

        }
        [HttpDelete("{ id:int}")]
        public IActionResult DeleteProducts(int id)
        {
            try
            {
                
                var result = _products.GetProductById(id);
                if (result != null) 
                {
                    
                    return Ok(_products.DeleteProducts(id));
                }
                return NotFound();


            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server is not responding !!!");
              
            }
        }
    }
}
