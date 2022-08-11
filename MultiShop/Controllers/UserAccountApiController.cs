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
    public class UserAccountApiController : ControllerBase
    {
        private readonly IUserAccountRepository _userAccount;
        public UserAccountApiController(IUserAccountRepository userAccount)
        {
            _userAccount = userAccount;
        }
        [HttpPost]
        public async Task<ActionResult> Register(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userAccount.CreateUserAsync(user);
                    if (result.Succeeded)
                    {
                        return Ok(result);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Credentials");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error While Creating New User");
            }
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(Login login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userAccount.Login(login);

                    return Ok(result);

                }

                else
                {
                    ModelState.AddModelError("", "Invalid Credentials");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error While Creating New User");
            }
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _userAccount.LogOut();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetUserId(string email)
        {
            return Ok(await _userAccount.GetLoginUserId(email));
        }
    }
}
