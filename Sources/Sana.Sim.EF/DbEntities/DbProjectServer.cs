﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.EF.DbEntities
{
    public class DbProjectServer : DbProjectEntity
    {
        public virtual Guid ServerId { get; set; }
    }
}
