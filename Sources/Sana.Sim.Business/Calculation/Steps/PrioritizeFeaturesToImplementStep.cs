using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.Business.Calculation.Steps
{
    public class PrioritizeFeaturesToImplementStep : ICalculationStep
    {
        public string Name => "PrioritizeFeaturesToImplement";

        public void Execute(CalculationContext context)
        {
            var helper = new FeaturesHelper();

            var random = new Random();

            var features = context.Project.Webshops.SelectMany(w => w.Features)
                .Where(f => !f.Implemented)
                .OrderByDescending(f => f.RemainingImplementationRequirements != f.Definition.ImplementationRequirements)
                .ThenByDescending(f => f.Definition.Type == BusinessConstants.FeatureTypes.Webshop)
                .ThenByDescending(f => f.Definition.Tag == "ERP")
                .ThenBy(f => helper.GetParentFeatures(f.Definition.Id).Count)
                .ThenBy(f => random.Next());

            context.FeaturesToImplement = features.ToList();
        }
    }
}
