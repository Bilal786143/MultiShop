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
    public class UserAccountApiControlller : ControllerBase
    {
        private readonly IUserAccountRepository _userAccount;
        public UserAccountApiControlller(IUserAccountRepository userAccount)
        {
            _userAccount = userAccount;

        }
        [HttpPost]
        public async Task<ActionResult>Register(User user)
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
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LogIn(Login login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userAccount.Login(login);
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
        [Route("LogOut")]
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _userAccount.LogOut();
            return Ok();
        }
        //public async Task<ActionResult<RegisterNewUser>> CreateNewUser(RegisterNewUser newUser)
        //{
        //    try
        //    {
        //        if (newUser == null)
        //        {
        //            return BadRequest();
        //        }

        //        var createNewUser = await _userAccount.CreateUserAsync(newUser);
        //        return Ok();

        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error While Creating New User");
        //    }

        //}
    }
}
