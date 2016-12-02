using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sana.Sim.Business.Entities.Resources;
using Sana.Sim.Business.Repositories;
using Sana.Sim.EF.DbEntities;

namespace Sana.Sim.EF.Repositories
{
    public class ServersRepository : BaseRepository<Server, DbServer>, IServersRepository
    {
    }
}
