using AutoMapper;
using Microsoft.AspNetCore.Http;
using Sana.Sim.Business;
using Sana.Sim.Business.Calculation;
using Sana.Sim.Business.Entities;
using Sana.Sim.Business.Entities.Features;
using Sana.Sim.Mvc.ViewModels.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.Mvc.Helpers
{
    public class BotHelper
    {
        public static Project GetCalculatedBotProject(Project project, int iterationsCount)
        {
            var botProject = BotHelper.CreateBotProject(project);

            return BotHelper.CalculateBotProject(iterationsCount, botProject);
        }

        private static Project CalculateBotProject(int iterationsCount, Project botProject)
        {
            var calculator = IoC.Resolve<ProgressCalculator>();
            for (int i = 0; i < iterationsCount; i++)
            {
                if (botProject.Webshops.Any())
                {
                    var webshop = botProject.Webshops.First();
                    var availableTime = BusinessConstants.DefaultDeveloperCapacity * 2;

                    while (webshop.Features.Sum(f => f.RemainingImplementationRequirements) < availableTime)
                    {
                        var implementedIds = webshop.Features.Select(f => f.Definition.Id).ToList();
                        var nextFeature = BotHelper.GetSortedFeaturesForBot().FirstOrDefault(f => !implementedIds.Contains(f.Id));

                        if (nextFeature == null)
                            break;

                        var nextWebshopFeature = new WebshopFeature()
                        {
                            Id = Guid.NewGuid(),
                            AddedOnIteration = i,
                            Definition = nextFeature,
                            WebshopId = webshop.Id,
                            RemainingImplementationRequirements = nextFeature.ImplementationRequirements
                        };

                        webshop.Features = webshop.Features.Concat(new[] { nextWebshopFeature }).ToList();
                    }

                }

                botProject = calculator.CalculateNextIteration(botProject, i);
            }

            return botProject;
        }

        private static Project CreateBotProject(Project project)
        {
            var botProject = Mapper.Map<Project>(new CreateProjectInputViewModel()
            {
                Budget = project.Budget,
                Name = "Bot"
            });

            botProject.Webshops = new List<Webshop>();

            if (project.Webshops.Any())
            {
                var webshop = project.Webshops.First();
                var botWebshop = Mapper.Map<Webshop>(new CreateWebshopInputViewModel()
                {
                    IterationNumber = webshop.WebshopFeature.AddedOnIteration,
                    Name = "bot: " + webshop.Name,
                    OfflineAverageOrderAmount = webshop.OfflineAverageOrderAmount,
                    OfflineServiceCostPercentage = webshop.OfflineServiceCostPercentage,
                    OfflineUsersCount = webshop.OfflineUsersCount,
                    OnlineServiceCostPercentage = webshop.OnlineServiceCostPercentage,
                    ProductCostPercentage = webshop.ProductCostPercentage,
                    Features = new[] { BusinessConstants.WebshopFeatureId.Standard }
                });

                botProject.Webshops = new List<Webshop>() { botWebshop };
            }

            return botProject;
        }

        private static List<Feature> GetSortedFeaturesForBot()
        {
            var list = IoC.Session.GetObject<List<Feature>>("BotFeatures");

            if (list == null)
            {
                var random = new Random();
                var helper = new FeaturesHelper();

                list = Framework.Features.Get()
                    .OrderByDescending(f => f.Type == BusinessConstants.FeatureTypes.Webshop)
                    .ThenByDescending(f => f.Tag == "ERP")
                    .ThenBy(f => helper.GetParentFeatures(f.Id).Count)
                    .ThenBy(f => random.Next()).ToList();
            }

            IoC.Session.SetObject("BotFeatures", list);

            return list;
        }
    }
}
