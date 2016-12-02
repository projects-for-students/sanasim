using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sana.Sim.Business.Entities;

namespace Sana.Sim.Business
{
    public class ExecutionContext
    {
        public Guid? ProjectId { get; set; }

        public bool Exists =>
            ProjectId.HasValue;

        public Project Project { get; set; }

        public int IterationNumber =>
            Project?.LatestChangeSet?.IterationNumber ?? 0;
    }
}
