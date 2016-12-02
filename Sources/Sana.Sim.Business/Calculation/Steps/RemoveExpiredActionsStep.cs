using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.Business.Calculation.Steps
{
    public class RemoveExpiredActionsStep : ICalculationStep
    {
        public string Name => "RemoveExpiredActions";

        public void Execute(CalculationContext context)
        {
            foreach (var webshop in context.Project.Webshops)
            {
                var expiredActions = webshop.Features
                    .Where(f => f.Definition.IsAction && f.AddedOnIteration + f.Definition.Duration < context.IterationNumber)
                    .ToList();

                expiredActions.ForEach(f =>
                {
                    f.Deleted = true;
                    f.LastUpdatedIteration = context.IterationNumber;
                });
            }
        }
    }
}
