using Microsoft.AspNetCore.Identity;
using MultiShop.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DataAccess.Infrastructure.IRepository
{
    public interface IUserAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(User user);
        Task<SignInResult> Login(Login login);
        Task LogOut();
    }
}
