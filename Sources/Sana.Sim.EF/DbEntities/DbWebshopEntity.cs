using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.EF.DbEntities
{
    public abstract class DbWebshopEntity : DbEntity
    {
        public virtual Guid WebshopId { get; set; }
    }
}
