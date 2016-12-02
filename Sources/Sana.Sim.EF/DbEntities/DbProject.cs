using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.EF.DbEntities
{
    public class DbProject : DbEntity
    {
        public virtual string Name { get; set; }

        public virtual decimal Budget { get; set; }
    }
}
