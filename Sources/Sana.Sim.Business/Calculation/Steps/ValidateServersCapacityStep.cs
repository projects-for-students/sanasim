using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.Business.Calculation.Steps
{
    public class ValidateServersCapacityStep : ICalculationStep
    {
        public string Name => "ValidateServersCapacity";

        public void Execute(CalculationContext context)
        {
            if (context.RequiredServersCapacity > context.ServersCapacity)
                context.Feedback.Add(string.Format("You do not have enough active servers. Only {0}% users will be handled.", Math.Ceiling((context.ServersCapacity / context.RequiredServersCapacity) * 100)));
        }
    }
}
