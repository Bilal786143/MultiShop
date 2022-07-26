using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DataAccess.Infrastructure.IRepository;
using MultiShop.Models.Models;
using System;
using System.Threading.Tasks;

namespace MultiShop.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryApiController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryApiController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                return Ok(await _categoryRepository.GetAllCategory());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error\nError While Retreiving Categories From Server");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            try
            {
                bool isExist = _categoryRepository.IsCategoryExist(id);
                if (!isExist)
                {
                    return NotFound();
                }

                return Ok(await _categoryRepository.GetCategoryById(id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var createCategory = await _categoryRepository.CreateCategory(category);
                    return CreatedAtAction(nameof(GetCategoryById), new { id = createCategory.Id }, createCategory);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error While Creating New Category");
            }
        }


        [HttpPost]
        public async Task<ActionResult<Category>> UpdateCategory(Category category)
        {
            try
            {
                bool result = _categoryRepository.IsCategoryExist(category.Id);
                if (!result)
                {
                    return NotFound($"Category With Requesting Details Like ID : {category.Id} not found");
                }
                return await _categoryRepository.UpdateCategory(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error While Updating Data at Current ID");
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCategoryById(int id)
        {
            try
            {
                bool isExist = _categoryRepository.IsCategoryExist(id);
                if (!isExist)
                {
                    return NotFound($"Category With Requesting Details (Category Id: {id}) to Delete Is not found");
                }
                await _categoryRepository.DeleteCategoryById(id);
                return Ok($"Category With ID : {id} is Delete Successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Deleting Category");
            }
        }

    }
}
