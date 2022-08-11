using Microsoft.AspNetCore.Identity;
using MultiShop.Models.Models;
using MultiShop.Models.Models.DTOs;
using MultiShop.Models.Response;
using System.Threading.Tasks;

namespace MultiShop.DataAccess.Infrastructure.IRepository
{
    public interface IUserAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(User user);
        Task<LoginResponse> Login(Login login);
        Task LogOut();
        //Task<GetUserId> GetLoginUserId();

        //string GetLoginUserId();
        Task<string> GetLoginUserId(string email);

    }
}
