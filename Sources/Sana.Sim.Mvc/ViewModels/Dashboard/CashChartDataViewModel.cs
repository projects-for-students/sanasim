using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sana.Sim.Business.Entities;
using Sana.Sim.Business.Entities.Features;
using Sana.Sim.Business;
using Microsoft.AspNetCore.Http;
using Sana.Sim.Business.Components;
using AutoMapper;
using Sana.Sim.Business.Calculation;
using Sana.Sim.Mvc.Helpers;

namespace Sana.Sim.Mvc.ViewModels.Dashboard
{
    public class CashChartDataViewModel
    {
        public IEnumerable<string> Iterations { get; set; }

        public IEnumerable<LineData> Lines { get; set; }

        public CashChartDataViewModel(Project project)
        {
            var iterationsCount = (project.LatestChangeSet?.IterationNumber ?? 0) + 1;

            this.Iterations = Enumerable.Range(0, iterationsCount).Select(r => "week " + r);

            var botBusinessLine = GetSuperBotBusinessLine(project, iterationsCount);
            var onlineBusinessLine = GetOnlineBusinessLine(project, iterationsCount);
            var offlineBusinessLine = GetOfflineBusinessLine(project, iterationsCount);

            this.Lines = new List<LineData>() { botBusinessLine, onlineBusinessLine, offlineBusinessLine }
            .Where(t => t != null).ToList();
        }

        private LineData GetSuperBotBusinessLine(Project project, int iterationsCount)
        {
            try
            {
                var botProject = BotHelper.GetCalculatedBotProject(project, iterationsCount);

                var line = new LineData("Bot");
                line.Values = Enumerable.Range(0, iterationsCount).Select(iteration =>
                {
                    var cash = botProject.Budget;

                    var projectChangeSets = botProject.Changes.Where(cs => cs.IterationNumber <= iteration).ToList();
                    projectChangeSets.ForEach(cs => cash -= cs.Cost);

                    foreach (var webshop in botProject.Webshops)
                    {
                        var webshopChangeSets = webshop.Changes.Where(cs => cs.IterationNumber <= iteration).ToList();
                        webshopChangeSets.ForEach(cs => cash += (cs.Income - cs.GetCost(webshop)));
                    }

                    return Math.Truncate(cash);
                }).ToList();
                return line;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private LineData GetOnlineBusinessLine(Project project, int iterationsCount)
        {
            var onlineBusinessLine = new LineData("Online business");
            onlineBusinessLine.Values = Enumerable.Range(0, iterationsCount).Select(iteration =>
            {
                var cash = project.Budget;

                var projectChangeSets = project.Changes.Where(cs => cs.IterationNumber <= iteration).ToList();
                projectChangeSets.ForEach(cs => cash -= cs.Cost);

                foreach (var webshop in project.Webshops)
                {
                    var webshopChangeSets = webshop.Changes.Where(cs => cs.IterationNumber <= iteration).ToList();
                    webshopChangeSets.ForEach(cs => cash += (cs.Income - cs.GetCost(webshop)));
                }

                return Math.Truncate(cash);
            }).ToList();
            return onlineBusinessLine;
        }

        private LineData GetOfflineBusinessLine(Project project, int iterationsCount)
        {
            var offlineBusinessLine = new LineData("Offline business");
            offlineBusinessLine.Values = Enumerable.Range(0, iterationsCount).Select(iteration =>
            {
                var cash = project.Budget;

                var webshop = project.Webshops.FirstOrDefault();
                if (webshop != null)
                {
                    for (int i = 0; i < iteration; i++)
                    {
                        cash += ((100 - webshop.ProductCostPercentage - webshop.OfflineServiceCostPercentage) / 100m * webshop.OfflineUsersCount * webshop.OfflineAverageOrderAmount) * (1 - i % 5 / 5m);
                    }
                }

                return Math.Truncate(cash);
            }).ToList();
            return offlineBusinessLine;
        }

        public class LineData
        {
            public string Name { get; set; }

            public IEnumerable<decimal> Values { get; set; }

            public LineData(string name)
            {
                this.Name = name;
            }
        }
    }
}
