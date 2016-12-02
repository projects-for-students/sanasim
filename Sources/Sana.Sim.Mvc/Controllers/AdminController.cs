using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sana.Sim.Mvc.Authentication;
using Sana.Sim.Mvc.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Sana.Sim.Mvc.ViewModels.Admin.Features;
using Sana.Sim.Business.Entities.Features;
using Sana.Sim.Business.Repositories;

namespace Sana.Sim.Mvc.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputViewModel model)
        {
            if (!await AdminAuthenticationManager.SignIn(HttpContext, model.UserName, model.Password))
            {
                ModelState.AddModelError("Unauthorized", "User name or password is not correct.");
                return View();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            AdminAuthenticationManager.SignOut(HttpContext);
            return RedirectToAction("Index", "Home");
        }
    }
}
