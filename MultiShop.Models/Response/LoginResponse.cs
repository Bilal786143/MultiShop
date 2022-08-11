using Microsoft.AspNetCore.Identity;
using MultiShop.Models.Models;

namespace MultiShop.Models.Response
{
    public class LoginResponse 
    {
        public string Email{ get; set; }
        public SignInResult Result { get; set; }
    }
}
