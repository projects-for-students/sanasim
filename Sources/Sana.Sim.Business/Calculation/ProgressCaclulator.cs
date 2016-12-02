using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Sana.Sim.Business.Calculation.Steps;
using Sana.Sim.Business.Entities;

namespace Sana.Sim.Business.Calculation
{
    public class ProgressCaclulator
    {
        /*
         * Calculation process:
         * 1. +Pay for developers (deducts cash)
         * 2. -Pay for servers (deducts cash)
         * 3. +Compute developer capacity (increases developer capacity)
         * 4. -Compute server capacity (increases server capacity)
         * 5. +Compute features build priorities (create ordered list of features to build)
         * 6. +Build features (decreases developer capacity)
         * 7. +Update online users count (increases online users count)
         * 8. +Update average order amount (increases average order amount)
         * 9. -Compute server load (decreases server capacity, if server capacity goes below 0, we introduce downtimes proportional to lacking server capacity)
         * 10. +-Calculate income (online users count * average order amount - penalty for downtime)
         *
         * add recurring features
         * add feature server load (maybe multipler for user load)
         */

        private List<ICalculationStep> steps;

        public ProgressCaclulator()
        {
            steps = new List<ICalculationStep>()
            {
                new InitializeStatesStep(),

                new PayForNewFeaturesStep(),


                new DetermineActiveServersStep(),

                new PayForServersStep(),
                //new PayForDevelopersStep(),

                new CalculateServersCapacityStep(),
                //new CalculateDevelopersCapacityStep(),
                new SetDefaultDevelopersCapacityStep(),

                new PrioritizeFeaturesToImplementStep(),
                new ExtractFeaturesWithImpactStep(),
                new ImplementFeaturesStep(),
                new CalculateOnlineUsersStep(),

                new CalculateRequiredServerCapacityStep(),
                new ValidateServersCapacityStep(),

                new CalculateAvarageOrderAmountStep(),

                new CalculateIncomeStep(),

                new RemoveExpiredActionsStep()
            };
        }

        private CalculationContext Calculate(Project project, int latestIterationNumber)
        {
            var calculationContext = new CalculationContext(project, latestIterationNumber);

            foreach (var step in steps)
            {
                step.Execute(calculationContext);
            }

            return calculationContext;
        }

        public Project CalculateNextIteration(Project project, int latestIterationNumber)
        {
            return Calculate(project, latestIterationNumber).Project;
        }

        public IEnumerable<string> GetPreCalculationFeedback(Project project, int latestIterationNumber)
        {
            return Calculate(project, latestIterationNumber).Feedback;
        }
    }
}
