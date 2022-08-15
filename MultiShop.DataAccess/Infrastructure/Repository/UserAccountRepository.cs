using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MultiShop.DataAccess.Infrastructure.IRepository;
using MultiShop.DataAccess.Services;
using MultiShop.Models.Models;
using MultiShop.Models.Response;
using System.Threading.Tasks;

namespace MultiShop.DataAccess.Infrastructure.Repository
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly UserManager<RegisterNewUser> _userManager;
        private readonly SignInManager<RegisterNewUser> _signInManager;
        private readonly IUserService _service;
        private readonly IHttpContextAccessor _httpContext;
        public UserAccountRepository(UserManager<RegisterNewUser> userManager,
                                     SignInManager<RegisterNewUser> signInManager, IUserService service)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _service = service;
        }

        public async Task<IdentityResult> CreateUserAsync(User user)
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
            };
            var result = await _userManager.CreateAsync(NewUser, user.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(NewUser, true);
                return result;
            }
            return null;
        }


        public async Task<LoginResponse> Login(Login login)
        {
            var response = new LoginResponse();
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, false);
            if (result.Succeeded)
            {
                response.Email = login.Email;
                response.Result = result;
                return response;
            }
            else
            {
                return response;
            }
        }
        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<string> GetLoginUserId(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user.Id;
            //var id = _signInManager.Context.User.Identity.Name;
            //var test = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
        }
    }
}
