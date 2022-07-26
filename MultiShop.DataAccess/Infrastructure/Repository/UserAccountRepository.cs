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
        private readonly UserManager<RegisterNewUser> _userManager;
        private readonly SignInManager<RegisterNewUser> _signInManager;
        public UserAccountRepository(UserManager<RegisterNewUser> userManager,
                                     SignInManager<RegisterNewUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }




        public async Task<IdentityResult> CreateUserAsync(User user)
        public async Task<IdentityResult> CreateUserAsync(RegisterNewUser newUser)
        {
            var NewUser = new RegisterNewUser()
            {
                Name = user.Name,
                Address = user.Address,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
               UserName = user.Email,
                Email = user.Email,
                ConfirmPasswrd = user.ConfirmPasswrd

                //Name = .Name,
                //Address = newUser.Address,
                //PhoneNumber=newUser.PhoneNumber,
                //Email = newUser.Email,
                //UserName = newUser.Email
                PhoneNumber = newUser.PhoneNumber,
                Email = newUser.Email,
                UserName = newUser.Email
            };

            var result = await _userManager.CreateAsync(NewUser, user.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(NewUser, true);
                return result;
            }
            return null;

         


            return null;
        }

        public async Task<SignInResult> Login(Login login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, false);
            return result;
        }
        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
