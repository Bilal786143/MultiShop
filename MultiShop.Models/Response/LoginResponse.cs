using Microsoft.AspNetCore.Identity;

namespace MultiShop.Models.Response
{
    public class LoginResponse
    {
        public string Email { get; set; }
        public SignInResult Result { get; set; }
    }
}
