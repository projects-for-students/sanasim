using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.Business.Calculation.Steps
{
    public class ExtractFeaturesWithImpactStep : ICalculationStep
    {
        public string Name => "ExtractFeaturesWithImpact";

        public void Execute(CalculationContext context)
        {
            var featuresWithImpact = context.Project.Webshops.SelectMany(w => w.Features)
                .Where(f => (f.Definition.IsFunctionality && f.Implemented && f.LastUpdatedIteration == context.IterationNumber - 1) ||
                            (f.Definition.IsAction && f.AddedOnIteration + f.Definition.Duration >= context.IterationNumber))
                .ToList();

            context.FeaturesWithImpact = featuresWithImpact;
        }
    }
}
