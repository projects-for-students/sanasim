using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.Business.Calculation.Steps
{
    public class PayForDevelopersStep : ICalculationStep
    {
        public string Name => "PayForDevelopers";

        public void Execute(CalculationContext context)
        {
            var developersCost = context.Project.Developers.Sum(d => d.Definition.Cost);

            if (context.Project.Cash < developersCost)
                throw new CalculationException(Name, "You do not have enough cash to pay for developers.");

            context.Project.LatestChangeSet.Cost += developersCost;
        }
    }
}
