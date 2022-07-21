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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly IAdminRepository _admin;
        public AdminApiController(IAdminRepository admin)
            {
                _admin = admin;
            }
        
        [HttpGet]
        public async Task<IActionResult> GetAdminList()
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
        public async Task<IActionResult> GetAdminById(int id)
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
        public async Task<IActionResult> CreateAdmin(Admin admin)
        {
            try
            {
                if (admin == null)
                {
                    return BadRequest();
                }
                var createAdmin = await _admin.CreateAdmin(admin);
                return CreatedAtAction(nameof(GetAdminById), new { id = createAdmin.Id }, createAdmin);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server is Not Rresponding");
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> EditAdmin(Admin admin)
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
        public IActionResult DeleteAdmin(int id)
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



