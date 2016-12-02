using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.Business.Calculation.Steps
{
    public class SetDefaultDevelopersCapacityStep : ICalculationStep
    {
        public string Name => "SetDefaultDevelopersCapacity";

        public void Execute(CalculationContext context)
        {
            context.DevelopersCapacity = BusinessConstants.DefaultDeveloperCapacity;
        }
    }
}
