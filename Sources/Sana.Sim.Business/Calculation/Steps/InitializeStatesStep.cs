using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Sana.Sim.Business.Components;
using Sana.Sim.Business.Entities;

namespace Sana.Sim.Business.Calculation.Steps
{
    public class InitializeStatesStep : ICalculationStep
    {
        public string Name => "InitializeStates";

        public void Execute(CalculationContext context)
        {
            var newProjectState = new ProjectChangeSet()
            {
                IterationNumber = context.IterationNumber
            };

            context.Project.Changes = context.Project.Changes.Concat(new[] { newProjectState }).ToList();

            foreach (var webshop in context.Project.Webshops)
            {
                var newWebshopState = new WebshopChangeSet()
                {
                    IterationNumber = context.IterationNumber
                };

                webshop.Changes = webshop.Changes.Concat(new[] { newWebshopState }).ToList();
            }
        }
    }
}
