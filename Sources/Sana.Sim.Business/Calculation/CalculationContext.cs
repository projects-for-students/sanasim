using System;
using System.Linq;
using System.Collections.Generic;
using Sana.Sim.Business.Entities;
using Sana.Sim.Business.Entities.Resources;

namespace Sana.Sim.Business.Calculation
{
    public class CalculationContext
    {
        public CalculationContext(Project project, int latestIterationNumber)
        {
            this.Project = project;
            this.IterationNumber = latestIterationNumber + 1;
            this.Feedback = new List<string>();
        }

        public Project Project { get; }

        public decimal DevelopersCapacity { get; set; }

        public decimal ServersCapacity { get; set; }

        public decimal RequiredServersCapacity { get; set; }

        public List<WebshopFeature> FeaturesToImplement { get; set; }

        public List<WebshopFeature> FeaturesWithImpact { get; set; }

        public List<Server> ActiveServers { get; set; }

        public int IterationNumber { get; }

        public List<string> Feedback { get; set; }
    }
}
