using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.Business.Entities.Resources
{
    public abstract class BaseResource
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Capacity { get; set; }

        public decimal Cost { get; set; }
    }
}
