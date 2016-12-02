using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sana.Sim.Business.Entities.Features;
using Sana.Sim.Business.Entities.Resources;

namespace Sana.Sim.Mvc.ViewModels.Admin
{
    public class EntityListViewModel<T>
    {
        public IEnumerable<T> Entities { get; set; }
    }
}
