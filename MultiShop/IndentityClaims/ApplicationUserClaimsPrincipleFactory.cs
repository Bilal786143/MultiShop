
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MultiShop.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityFramwork.Claims
{
    public class ApplicationUserClaimsPrincipleFactory: UserClaimsPrincipalFactory<RegisterNewUser,IdentityRole>
    {
        public ApplicationUserClaimsPrincipleFactory(UserManager<RegisterNewUser> userManager,RoleManager<IdentityRole> roleManager,IOptions<IdentityOptions> options):base(userManager,roleManager,options)

        {

        }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(RegisterNewUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("UserName", user.Name ?? string.Empty));
          
            return identity;
        }
    }

}
