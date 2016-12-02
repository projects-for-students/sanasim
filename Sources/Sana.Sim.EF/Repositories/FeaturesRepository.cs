using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sana.Sim.Business.Entities.Features;
using Sana.Sim.Business;
using Sana.Sim.EF.DbAccess;
using AutoMapper;
using Sana.Sim.EF.DbEntities;
using Sana.Sim.EF.Repositories;
using Sana.Sim.Business.Repositories;

namespace Sana.Sim.EF.Repositories
{
    public class FeaturesRepository : BaseRepository<Feature, DbFeature>, IFeaturesRepository
    {
    }
}
