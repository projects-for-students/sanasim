using Sana.Sim.Business.Entities.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.Business.Entities
{
    public class WebshopFeature
    {
        public Guid Id { get; set; }

        public Feature Definition { get; set; }

        public bool Implemented
        {
            get { return RemainingImplementationRequirements <= 0; }
        }

        public decimal RemainingImplementationRequirements { get; set; }

        public int LastUpdatedIteration { get; set; }

        public int AddedOnIteration { get; set; }

        public Guid WebshopId { get; set; }

        public bool Deleted { get; set; }

        public int BuildProgress =>
            (int)((Definition.ImplementationRequirements - RemainingImplementationRequirements) / Definition.ImplementationRequirements * 100);
    }
}
