using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.EF.DbEntities
{
    public class DbWebshopFeature : DbWebshopEntity
    {
        public virtual Guid FeatureId { get; set; }

        public virtual decimal RemainingImplementationRequirements { get; set; }

        public virtual int LastUpdatedIteration { get; set; }

        public virtual int AddedOnIteration { get; set; }
    }
}
