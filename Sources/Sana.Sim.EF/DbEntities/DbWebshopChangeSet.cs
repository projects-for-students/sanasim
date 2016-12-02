using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.EF.DbEntities
{
    public class DbWebshopChangeSet : DbWebshopEntity
    {
        public virtual decimal NewUsersCount { get; set; }

        public virtual decimal ConvertedUsersCount { get; set; }

        public virtual decimal AverageOrderAmountIncreaseRate { get; set; }

        public virtual int IterationNumber { get; set; }

        public virtual decimal Income { get; set; }
    }
}
