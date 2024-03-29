﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Forward.Areas.Identity.Data
{
    public static class MyIdentityDataInitializer
    {
        public static void SeedUsers (UserManager<IdentityUser> userManager) {
            if (userManager.FindByNameAsync("normal").Result == null) {
                IdentityUser user = new IdentityUser {
                    UserName = "normal@mail.com",
                    Email = "normal@mail.com",
                    EmailConfirmed = true
                };

                _ = userManager.CreateAsync(user, @"PZg38F#@7oOVvrH").Result;
            }

            if (userManager.FindByNameAsync("admin").Result == null) {
                IdentityUser user = new IdentityUser {
                    UserName = "admin@mail.com",
                    Email = "admin@mail.com",
                    EmailConfirmed = true
                };

                _ = userManager.CreateAsync(user, @"PZg38F#@7oOVvrH").Result;

            }
        }
    }
}
