using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.EF.DbEntities
{
    public class DbProjectDeveloper : DbProjectEntity
    {
        public virtual Guid DeveloperId { get; set; }
    }
}
