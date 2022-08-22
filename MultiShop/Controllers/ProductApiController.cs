using Microsoft.AspNetCore.Mvc;
using MultiShop.DataAccess.Infrastructure.IRepository;
using MultiShop.Models.Request;
using MultiShop.Models.Models;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace MultiShop.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductApiController : BaseController
    {
        private readonly IProductRepository _products;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductApiController(IProductRepository products, IWebHostEnvironment webHostEnvironment)
        {
            _products = products;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsList()
        {
            try
            {
                var product = await _products.GetProducts();
                foreach (var item in product)
                {
                    item.ProductImagePath = _webHostEnvironment.WebRootPath + item.ProductImagePath;
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse(ex));
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
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse(ex));
            }
        }
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProducts([FromForm] ProductCreateRequest product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string folder = "\\ProductImage\\";
                    string fileName = Guid.NewGuid() + product.ProductImage.FileName;
                    if (product.ProductImage.Length > 0)
                    {
                        if (!Directory.Exists(_webHostEnvironment.WebRootPath + "\\ProductImage\\"))
                        {
                            Directory.CreateDirectory(_webHostEnvironment.WebRootPath + "\\ProductImage\\");
                        }
                        using (FileStream fileStream = System.IO.File.Create(_webHostEnvironment.WebRootPath + folder + fileName))
                        {

                            await product.ProductImage.CopyToAsync(fileStream);
                            await fileStream.FlushAsync();
                        }
                    }
                    var picPath = folder + fileName;
                    var createProduct = await _products.CreateProduct(product, picPath);
                    createProduct.ProductImagePath = _webHostEnvironment.WebRootPath + createProduct.ProductImagePath;
                    return Ok(createProduct);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse(ex));
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
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse(ex));
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
                return Ok(await _products.DeleteProducts(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse(ex));
            }
        }
    }
}
