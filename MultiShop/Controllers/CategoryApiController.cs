using Microsoft.AspNetCore.Mvc;
using MultiShop.DataAccess.Infrastructure.IRepository;
using MultiShop.Models.Models;
using System;
using System.Threading.Tasks;

namespace MultiShop.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class CategoryApiController : BaseController
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
            catch (Exception ex)
            {
                return BadRequest( ErrorResponse(ex));
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
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse(ex));
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
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse(ex));
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
                    return NotFound();
                }
                return Ok(await _categoryRepository.UpdateCategory(category));
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse(ex));
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
                    return NotFound();
                }
               return Ok( await _categoryRepository.DeleteCategoryById(id));
             
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse(ex));
            }
        }

    }
}
