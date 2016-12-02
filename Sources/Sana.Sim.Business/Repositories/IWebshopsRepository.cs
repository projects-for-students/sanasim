using Sana.Sim.Business.Entities.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sana.Sim.Business.Entities;

namespace Sana.Sim.Business.Repositories
{
    public interface IWebshopsRepository
    {
        Webshop Get(Guid id);

        IEnumerable<Webshop> GetForProject(Guid projectId);

        void Create(Webshop entity);

        void AddFeatures(Guid webshopId, IEnumerable<Guid> featureIds);
    }
}
