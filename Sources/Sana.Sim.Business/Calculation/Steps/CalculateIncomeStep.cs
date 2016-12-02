using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sana.Sim.Business.Entities;

namespace Sana.Sim.Business.Calculation.Steps
{

#warning "Implement downtime multiplier calculation!"
    public class CalculateIncomeStep : ICalculationStep
    {
        public string Name => "CalculateIncome";

        public void Execute(CalculationContext context)
        {
            foreach (var webshop in context.Project.Webshops)
                ExecutePerWebshop(webshop, context);
        }

        public void ExecutePerWebshop(Webshop webshop, CalculationContext context)
        {
            var activeUsersCount = webshop.UserCount;
            if (context.RequiredServersCapacity > context.ServersCapacity)
                activeUsersCount *= (context.ServersCapacity / context.RequiredServersCapacity);

            var income = Math.Ceiling(webshop.AverageOrderAmount * activeUsersCount);
            webshop.LatestChangeSet.Income = income;
        }
    }
}
