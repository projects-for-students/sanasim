using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Authentication;

namespace Sana.Sim.Mvc.Authentication
{
    public static class AdminAuthenticationManager
    {
        public const string AdminAuthenticationScheme = "AdminCookieMiddlewareInstance";

        public static async Task<bool> SignIn(HttpContext context, string userName, string password)
        {
            if (!CanAuthenticate(userName, password))
                return false;

            var identity = new GenericIdentity(userName, "Admin");
            var principal = new ClaimsPrincipal(identity);

            await context.Authentication.SignInAsync(AdminAuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = true });

            return true;
        }

        public static async void SignOut(HttpContext context)
        {
            await context.Authentication.SignOutAsync(AdminAuthenticationScheme);
        }

        private static bool CanAuthenticate(string userName, string password)
        {
            return string.Equals(userName, "admin", StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals(password, "admin", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
