using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sana.Sim.Business;
using Sana.Sim.Business.Repositories;

namespace Sana.Sim.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index([FromServices]ExecutionContext executionContext, [FromServices]IFeaturesRepository featuresRepository)
        {
            var features = featuresRepository.Get();

            if (executionContext.Exists)
                return RedirectToAction("Index", "Dashboard");

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
