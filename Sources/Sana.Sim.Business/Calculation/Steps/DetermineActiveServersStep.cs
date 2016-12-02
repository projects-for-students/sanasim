using Sana.Sim.Business.Entities.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.Business.Calculation.Steps
{
    public class DetermineActiveServersStep : ICalculationStep
    {
        public string Name => "DetermineActiveServers";

        public void Execute(CalculationContext context)
        {
            context.ActiveServers = new List<Server>(context.Project.Servers.Select(ps => ps.Definition));
            while (context.ActiveServers.Any() && context.ActiveServers.Sum(s => s.Cost) > context.Project.Cash)
            {
                context.ActiveServers.RemoveAt(0);
            }

            if (context.ActiveServers.Count != context.Project.Servers.Count())
                context.Feedback.Add("You don't have enough money to pay for all your servers.");
        }
    }
}
