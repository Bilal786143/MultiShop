using Microsoft.AspNetCore.Identity;
using MultiShop.DataAccess.Infrastructure.IRepository;
using MultiShop.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DataAccess.Infrastructure.Repository
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public UserAccountRepository(UserManager<IdentityUser> userManager,
                                     SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> CreateUserAsync(RegisterNewUser newUser)
        {
            var user = new IdentityUser()
            {
                PhoneNumber = newUser.PhoneNumber,
                Email = newUser.Email,
                UserName = newUser.Email
            };

            var result = await _userManager.CreateAsync(user, newUser.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, true);
                return result;
            }
            return null;
        }
    }
}
