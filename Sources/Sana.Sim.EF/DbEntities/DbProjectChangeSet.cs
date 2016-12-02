using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.EF.DbEntities
{
    public class DbProjectChangeSet : DbProjectEntity
    {
        public virtual int IterationNumber { get; set; }
        
        public virtual decimal Cost { get; internal set; }
    }
}
