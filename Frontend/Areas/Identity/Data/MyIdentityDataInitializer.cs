using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Forward.Areas.Identity.Data
{
    public static class MyIdentityDataInitializer
    {

        public static void SeedUsers (UserManager<IdentityUser> userManager) {
            if (userManager.FindByNameAsync("admin").Result == null) {
                IdentityUser user = new IdentityUser {
                    UserName = "admin",
                    Email = "upperiq@hotmail.com"
                };

                _ = userManager.CreateAsync(user, "password").Result;
            }
        }
    }
}
