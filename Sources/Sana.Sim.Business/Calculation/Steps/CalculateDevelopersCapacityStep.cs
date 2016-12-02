using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.Business.Calculation.Steps
{
    public class CalculateDevelopersCapacityStep : ICalculationStep
    {
        public string Name => "CalculateDevelopersCapacity";

        public void Execute(CalculationContext context)
        {
            context.DevelopersCapacity = context.Project.Developers.Sum(d => d.Definition.Capacity);
        }
    }
}
