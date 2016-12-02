using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sana.Sim.Business.Entities;

namespace Sana.Sim.Business.Calculation.Steps
{
    public class CalculateAvarageOrderAmountStep : ICalculationStep
    {
        public string Name => "CalculateAvarageOrderAmount";

        public void Execute(CalculationContext context)
        {
            foreach (var webshop in context.Project.Webshops)
                ExecutePerWebshop(webshop, context);
        }

        public void ExecutePerWebshop(Webshop webshop, CalculationContext context)
        {
            var increaseRate = context.FeaturesWithImpact
                .Where(f => f.WebshopId == webshop.Id)
                .Sum(f => (f.Definition.AverageOrderAmountIncreaseRate ?? 0) / 100m);

            webshop.LatestChangeSet.AverageOrderAmountIncreaseRate = increaseRate;
        }
    }
}
