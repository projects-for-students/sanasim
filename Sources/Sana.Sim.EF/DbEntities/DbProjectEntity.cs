﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.EF.DbEntities
{
    public abstract class DbProjectEntity : DbEntity
    {
        public virtual Guid ProjectId { get; set; }
    }
}
