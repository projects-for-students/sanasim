using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.Business.Calculation.Steps
{
    public class PayForServersStep : ICalculationStep
    {
        public string Name => "PayForServers";

        public void Execute(CalculationContext context)
        {
            var serversCost = context.ActiveServers.Sum(s => s.Cost);
            
            context.Project.LatestChangeSet.Cost += serversCost;
        }
    }
}
