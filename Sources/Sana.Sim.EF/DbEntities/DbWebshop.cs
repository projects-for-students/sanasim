using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.EF.DbEntities
{
    public class DbWebshop : DbProjectEntity
    {
        public virtual string Name { get; set; }

        public virtual decimal OfflineUsersCount { get; set; }

        public virtual decimal OfflineAverageOrderAmount { get; set; }

        public virtual decimal ProductCostPercentage { get; set; }

        public virtual decimal OfflineServiceCostPercentage { get; set; }

        public virtual decimal OnlineServiceCostPercentage { get; set; }
    }
}
