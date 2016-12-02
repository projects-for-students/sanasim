using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.EF.DbEntities
{
    public class DbFeature : DbEntity
    {
        public virtual string Tag { get; set; }
               
        public virtual string Name { get; set; }
               
        public virtual Guid? ParentId { get; set; }

        public virtual string Type { get; set; }

        public virtual decimal ImplementationRequirements { get; set; }

        public virtual decimal ImplementationCost { get; set; }

        public virtual decimal? Duration { get; set; }

        public virtual decimal? OfflineUsersConversionRate { get; set; }
               
        public virtual decimal? NewOnlineUsersAmount { get; set; }

        public virtual decimal? AverageOrderAmountIncreaseRate { get; set; }
    }
}
