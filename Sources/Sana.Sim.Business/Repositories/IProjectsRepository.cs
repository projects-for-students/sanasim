using Sana.Sim.Business.Entities.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sana.Sim.Business.Entities;

namespace Sana.Sim.Business.Repositories
{
    public interface IProjectsRepository
    {
        Project Get(Guid id);

        void Create(Project entity);

        void AddDeveloper(Guid projectId, Guid developerId);

        void AddServer(Guid projectId, Guid serverId);

        void SaveIterationData(Project project, int iterationNumber);
    }
}
