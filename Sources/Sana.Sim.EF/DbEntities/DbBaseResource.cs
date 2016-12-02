using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.EF.DbEntities
{
    public abstract class DbBaseResource : DbEntity
    {
        public virtual string Name { get; set; }

        public virtual decimal Capacity { get; set; }

        public virtual decimal Cost { get; set; }
    }
}
