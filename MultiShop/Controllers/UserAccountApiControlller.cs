using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DataAccess.Infrastructure.IRepository;
using MultiShop.Models.Models;
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
        public async Task<ActionResult> index(RegisterNewUser user)
        {
            var result=await _userAccount.CreateUserAsync(user);
            return Ok(result);

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
