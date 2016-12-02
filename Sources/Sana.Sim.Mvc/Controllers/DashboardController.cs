using Microsoft.AspNetCore.Mvc;
using Sana.Sim.Mvc.Helpers;
using Sana.Sim.Mvc.ServiceFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sana.Sim.Business.Entities;
using Sana.Sim.Mvc.ViewModels.Dashboard;
using AutoMapper;
using Sana.Sim.Business;
using Sana.Sim.Business.Calculation;

namespace Sana.Sim.Mvc.Controllers
{
#warning add [ServiceFilter(typeof(ExecutionContextRequiredFilter))]
    public class DashboardController : Controller
    {
        private WebshopReportViewModel CreateWebshopReportModel(Webshop webshop)
        {
            if (webshop.LatestChangeSet == null)
                return null;

            var report = new WebshopReportViewModel();

            report.WebshopName = webshop.Name;
            report.WentLive = webshop.IsOnLine && webshop.WebshopFeature.LastUpdatedIteration == Framework.Context.IterationNumber;

            report.FinishedFeatures = webshop.Features.Where(f => f.Implemented && f.Definition.IsFunctionality && f.LastUpdatedIteration == Framework.Context.IterationNumber)
                .Select(f => f.Definition.Name).ToList();

            report.RunningActions = webshop.Features.Where(f => f.Implemented && f.Definition.IsAction && f.AddedOnIteration + f.Definition.Duration >= Framework.Context.IterationNumber)
                .Select(f => f.Definition.Name).ToList();

            var inprogressFeature = webshop.Features
                .SingleOrDefault(f => !f.Implemented && f.LastUpdatedIteration == Framework.Context.IterationNumber && f.RemainingImplementationRequirements != f.Definition.ImplementationRequirements);
            if (inprogressFeature != null)
            {
                var remainingPeriodsCount = Math.Ceiling(inprogressFeature.RemainingImplementationRequirements / BusinessConstants.DefaultDeveloperCapacity);
                report.InProgressFeature = string.Format("{0} (Remaining weeks = {1})", inprogressFeature.Definition.Name, remainingPeriodsCount);
            }

            report.ConvertedUsersCountDifference = webshop.LatestChangeSet.ConvertedUsersCount;
            report.NewUsersCountDifference = webshop.LatestChangeSet.NewUsersCount;
            report.AverageOrderAmountDifference = webshop.LatestChangeSet.AverageOrderAmountIncreaseRate;

            return report;
        }

        private ReportViewModel CreateReportModel(Project project)
        {
            if (Framework.Context.IterationNumber == 0)
                return null;

            var report = new ReportViewModel();

            report.WebshopReports = project.Webshops.Select(CreateWebshopReportModel).Where(r => r != null).ToList();

            return report;
        }

        [HttpGet]
        [ServiceFilter(typeof(ExecutionContextRequiredFilter))]
        public IActionResult Index()
        {
            if (!Framework.Context.Project.Webshops.Any())
                return RedirectToAction("CreateWebshop");

            var model = new DashboardViewModel
            {
                Webshops = Framework.Context.Project.Webshops,
                Report = CreateReportModel(Framework.Context.Project)
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Webshop(Guid id)
        {
            var model = new WebshopDetailsViewModel()
            {
                Webshop = Framework.Context.Project.Webshops.Single(w => w.Id == id)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Webshop(Guid id, IEnumerable<Guid> features)
        {
            var webshop = Framework.Context.Project.Webshops.Single(w => w.Id == id);
            if (!IsFeaturesSelectionValid(webshop.Features.Select(f => f.Definition.Id).Concat(features)))
                return RedirectToAction("Webshop", new { id = id });

            Framework.Webshops.AddFeatures(id, features ?? new Guid[0]);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Actions(Guid id)
        {
            var model = new WebshopDetailsViewModel()
            {
                Webshop = Framework.Context.Project.Webshops.Single(w => w.Id == id)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Actions(Guid id, IEnumerable<Guid> features)
        {
            var webshop = Framework.Context.Project.Webshops.Single(w => w.Id == id);
            if (!IsFeaturesSelectionValid(webshop.Features.Select(f => f.Definition.Id).Concat(features)))
                return RedirectToAction("Webshop", new { id = id });

            Framework.Webshops.AddFeatures(id, features ?? new Guid[0]);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StartGame([FromServices] ExecutionContextKeyHelper keyHelper)
        {
            var executionContextId = Guid.NewGuid();
            keyHelper.SetExecutionContextId(executionContextId);

            var model = new CreateProjectInputViewModel() { Name = DateTime.Now.ToString(), Budget = 100000 };
            var project = Mapper.Map<Project>(model);
            project.Id = executionContextId;
            Framework.Projects.Create(project);

            return RedirectToAction("CreateWebshop");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EndGame([FromServices] ExecutionContextKeyHelper keyHelper)
        {
            keyHelper.ClearExecutionContextId();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CreateWebshop()
        {
            var model = new CreateWebshopInputViewModel()
            {
                ProductCostPercentage = 80,
                OfflineServiceCostPercentage = 2,
                OnlineServiceCostPercentage = 1
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateWebshop(CreateWebshopInputViewModel model)
        {
            if (!ModelState.IsValid || !IsFeaturesSelectionValid(model.Features))
                return View(model);

            if (model.Features == null)
                model.Features = new List<Guid>();
            model.Features = model.Features.Concat(new[] { BusinessConstants.WebshopFeatureId.Standard });
            model.IterationNumber = Framework.Context.IterationNumber;

            var webshop = Mapper.Map<Webshop>(model);
            webshop.ProjectId = Framework.Context.ProjectId.Value;
            Framework.Webshops.Create(webshop);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ManageDevelopers()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PaybackCalculator([FromServices] ExecutionContextKeyHelper keyHelper)
        {
            // Ending existing game if there is any
            keyHelper.ClearExecutionContextId();

            // Starting new game
            var executionContextId = Guid.NewGuid();
            keyHelper.SetExecutionContextId(executionContextId);

            var defaultGame = new CreateProjectInputViewModel() { Name = DateTime.Now.ToString(), Budget = 100000 };
            var project = Mapper.Map<Project>(defaultGame);
            project.Id = executionContextId;
            Framework.Projects.Create(project);

            var model = new CreatePaybackCalculationWebshopInputViewModel()
            {
                Name = "Payback calculation example",
                ProductCostPercentage = 80,
                OfflineServiceCostPercentage = 2,
                OnlineServiceCostPercentage = 1
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult PaybackCalculator(CreatePaybackCalculationWebshopInputViewModel model)
        {
            if (!ModelState.IsValid || !IsFeaturesSelectionValid(model.Features))
                return View(model);

            if (model.Features == null)
                model.Features = new List<Guid>();
            model.Features = model.Features.Concat(new[] { BusinessConstants.WebshopFeatureId.Standard });
            model.IterationNumber = Framework.Context.IterationNumber;
            model.OfflineUsersCount = model.GetOfflineUsersCount();
            model.OfflineAverageOrderAmount = model.AverageOrderValue;

            var webshop = Mapper.Map<Webshop>(model);
            webshop.ProjectId = Framework.Context.ProjectId.Value;
            Framework.Webshops.Create(webshop);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDeveloper(Guid developerId)
        {
            if (ModelState.IsValid)
            {
                Framework.Projects.AddDeveloper(Framework.Context.ProjectId.Value, developerId);
                return RedirectToAction("ManageDevelopers");
            }

            return View();
        }

        [HttpGet]
        public IActionResult ManageServers()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddServer(Guid serverId)
        {
            if (ModelState.IsValid)
            {
                Framework.Projects.AddServer(Framework.Context.ProjectId.Value, serverId);
                return RedirectToAction("ManageServers");
            }

            return View();
        }

        private bool IsFeaturesSelectionValid(IEnumerable<Guid> ids)
        {
            if (ids != null)
            {
                var featuresHelper = new FeaturesHelper();

                foreach (var featureId in ids)
                {
                    var parents = featuresHelper.GetParentFeatures(featureId);
                    if (parents.Any(parent => !ids.Contains(parent.Id)))
                    {
                        ModelState.AddModelError(nameof(ids), "Invalid selection");

                        return false;
                    }
                }
            }

            return true;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SkipWeek()
        {
            var calculator = IoC.Resolve<ProgressCalculator>();
            var project = calculator.CalculateNextIteration(Framework.Context.Project, Framework.Context.IterationNumber);
            Framework.Projects.SaveIterationData(project, project.LatestChangeSet.IterationNumber);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult GetPreCalculationFeedback()
        {
            var calculator = IoC.Resolve<ProgressCalculator>();
            var feedback = calculator.GetPreCalculationFeedback(Framework.Context.Project, Framework.Context.IterationNumber);
            return Json(feedback);
        }

        [HttpPost]
        public IActionResult CashChartData()
        {
            var model = new CashChartDataViewModel(Framework.Context.Project);

            return Json(model);
        }

        [HttpPost]
        public IActionResult MarketChartData()
        {
            var model = new CashChartDataViewModel(Framework.Context.Project);

            var botUsers = BotHelper.GetCalculatedBotProject(Framework.Context.Project, Framework.Context.IterationNumber).Webshops.Sum(w => w.UserCount);
            var playerUsers = Framework.Context.Project.Webshops.Sum(w => w.UserCount);

            var res = new[]
            {
                new { label = "You", data = Math.Truncate(playerUsers)},
                new { label = "Bot", data = Math.Truncate(botUsers)},
                new { label = "Available market", data = Math.Truncate((playerUsers+botUsers)/3)}
            };

            return Json(res);
        }
    }
}
