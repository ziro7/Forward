using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Forward
{
    public class LoginModel : PageModel
    {
        // This is not a blazor razor page, but just a normal razor page as it lives outside the context of blazor
        [BindProperty]
        public string Email { get; set; }
        
        [BindProperty]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public async Task<IActionResult> OnPostAsync() {

            // To do - add some library - maybe with hashing of passwords?
            if(!(Email=="upperiq@hotmail.com" && Password == "password")) {
                return Page();
            }

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name,"Jacob"),
                new Claim(ClaimTypes.Email, Email),
            };

            // Create the identity with the claims above and the autentication type which was set in startup (they have to match to work)
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Create actual cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return LocalRedirect(Url.Content("~/"));
        }
    }
}