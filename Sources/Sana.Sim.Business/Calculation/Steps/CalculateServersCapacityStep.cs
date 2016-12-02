using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.Business.Calculation.Steps
{
    public class CalculateServersCapacityStep : ICalculationStep
    {
        public string Name => "CalculateServersCapacity";

        public void Execute(CalculationContext context)
        {
            context.ServersCapacity = context.ActiveServers.Sum(d => d.Capacity);
        }
    }
}
