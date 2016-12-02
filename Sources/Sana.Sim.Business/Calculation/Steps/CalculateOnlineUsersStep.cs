using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sana.Sim.Business.Entities;

namespace Sana.Sim.Business.Calculation.Steps
{
    public class CalculateOnlineUsersStep : ICalculationStep
    {
        public string Name => "CalculateOnlineUsers";

        public void Execute(CalculationContext context)
        {
            foreach (var webshop in context.Project.Webshops)
                ExecutePerWebshop(webshop, context);
        }

        public void ExecutePerWebshop(Webshop webshop, CalculationContext context)
        {
            webshop.LatestChangeSet.NewUsersCount = context.FeaturesWithImpact.Where(f => f.WebshopId == webshop.Id)
                .Sum(f => (f.Definition.NewOnlineUsersAmount ?? 0) / 100 * webshop.UserCount);

            var conversionRate = context.FeaturesWithImpact.Where(f => f.WebshopId == webshop.Id)
                .Sum(f => (f.Definition.OfflineUsersConversionRate ?? 0) / 100m);

            webshop.LatestChangeSet.ConvertedUsersCount = Math.Min(Math.Ceiling(conversionRate * webshop.OfflineUsersCount), webshop.OfflineUsersCount - webshop.ConvertedUsersCount);
        }
    }
}
