using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.Business.Calculation.Steps
{
    public class ImplementFeaturesStep : ICalculationStep
    {
        public string Name => "ImplementFeatures";

        public void Execute(CalculationContext context)
        {
            while (context.FeaturesToImplement.Any() && context.DevelopersCapacity > 0)
            {
                var feature = context.FeaturesToImplement.First();
                var actualCapacity = Math.Min(feature.RemainingImplementationRequirements, context.DevelopersCapacity);

                feature.RemainingImplementationRequirements -= actualCapacity;
                feature.LastUpdatedIteration = context.IterationNumber;
                context.DevelopersCapacity -= actualCapacity;

                context.FeaturesToImplement.Remove(feature);
            }            
        }
    }
}
