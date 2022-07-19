using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DataAccess.Infrastructure.IRepository;
using MultiShop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        public class ProductApiController : ControllerBase
        {
            private readonly IAdminRepository _admin;

            public ProductApiController(IAdminRepository admin)
            {
                _admin = admin;
            }
            [HttpGet]
            public async Task<IActionResult> GetProductsList()
            {
                try
                {
                    return Ok(await _admin.GetAllAdmin());
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
                        return BadRequest($"This ID {id} Could not found please enter a valid ID");
                    }
                    else return Ok(await _admin.GetAdminById(id));

                }
                catch (Exception)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError, "Server is not responding ");
                }
            }
            [HttpPost]
            public async Task<IActionResult> CreateProducts(Admin admin )
            {
                try
                {
                    if (admin == null)
                    {
                        return NotFound();
                    }
                    else return
                   Ok(await _admin.CreateAdmin(admin));
                }
                catch (Exception)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError, "Server is Not Rresponding");
                }

            }
            [HttpPost]
            public async Task<IActionResult> EditProducts(Admin admin)
            {
                try
                {
                    if (admin == null)
                    {
                        return NotFound();

                    }
                    else
                    {
                        return Ok(await _admin.EditAdmin(admin));
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

                    var result =  _admin.GetAdminById(id);
                    if (result != null)
                    {

                        return Ok(_admin.DeleteAdmin(id));
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

}

