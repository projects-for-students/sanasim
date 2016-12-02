using Sana.Sim.Business.Entities.Features;
using Sana.Sim.Business.Entities.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.Business.Repositories
{
    public interface IEntityRepository<T>
    {
        T Get(Guid id);

        IEnumerable<T> Get();

        void Create(T entity);

        void Update(T entity);

        void Delete(Guid id);
    }
}
