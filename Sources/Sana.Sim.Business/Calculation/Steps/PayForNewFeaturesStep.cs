using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sana.Sim.Business.Entities;

namespace Sana.Sim.Business.Calculation.Steps
{
    public class PayForNewFeaturesStep : ICalculationStep
    {
        public string Name => "PayForNewFeatures";

        public void Execute(CalculationContext context)
        {
            foreach (var webshop in context.Project.Webshops)
            {
                ExecutePerWebshop(context, webshop);
            }
        }

        private void ExecutePerWebshop(CalculationContext context, Webshop webshop)
        {
            var newFeatures = webshop.Features.Where(wf => wf.AddedOnIteration == context.IterationNumber - 1).ToList();

            var newFeaturesCost = newFeatures.Sum(f => f.Definition.ImplementationRequirements * BusinessConstants.PricePerHour);

            if (context.Project.Cash < newFeaturesCost)
                throw new CalculationException(Name, "You do not have enough cash to pay for new features.");

            context.Project.LatestChangeSet.Cost += newFeaturesCost;
        }
    }
}
