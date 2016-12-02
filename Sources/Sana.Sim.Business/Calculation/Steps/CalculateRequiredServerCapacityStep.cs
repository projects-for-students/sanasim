using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sana.Sim.Business.Entities;

namespace Sana.Sim.Business.Calculation.Steps
{
    public class CalculateRequiredServerCapacityStep : ICalculationStep
    {
        public string Name => "CalculateRequiredServerCapacity";

        public void Execute(CalculationContext context)
        {
            foreach (var webshop in context.Project.Webshops)
            {
                context.RequiredServersCapacity += (BusinessConstants.RequiredServerCapacityPerUser * webshop.UserCount);
            }
        }
    }
}
