using Sana.Sim.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sana.Sim.Business.Entities.Features;
using Sana.Sim.Business;
using Sana.Sim.EF.DbAccess;
using AutoMapper;
using Sana.Sim.EF.DbEntities;
using Sana.Sim.Business.Entities;

namespace Sana.Sim.EF.Repositories
{
    public class WebshopsRepository : BaseRepository<Webshop, DbWebshop>, IWebshopsRepository
    {
        public IEnumerable<Webshop> GetForProject(Guid projectId)
        {
            using (var context = CreateDataContext())
            {
                var query = context.Webshops.Where(w => w.ProjectId == projectId);
                var entities = Map(query).ToList();
                entities.ForEach(e => PostLoad(context, e));

                return entities;
            }
        }

        protected override void PostLoad(DataContext context, Webshop entity)
        {
            if (entity == null)
                return;

            var stateQuery = context.Set<DbWebshopChangeSet>().Where(s => s.WebshopId == entity.Id).OrderBy(s => s.IterationNumber);
            entity.Changes = Map<DbWebshopChangeSet, WebshopChangeSet>(stateQuery);

            var featuresQuery = context.Set<DbWebshopFeature>().Where(s => s.WebshopId == entity.Id);
            entity.Features = Map<DbWebshopFeature, WebshopFeature>(featuresQuery);
        }

        protected override void PostCreate(DataContext context, Webshop entity)
        {
            var states = Map<WebshopChangeSet, DbWebshopChangeSet>(entity.Changes).ToList();
            states.ForEach(item => item.WebshopId = entity.Id);
            context.AddRange(states);

            var includedFeatures = Map<WebshopFeature, DbWebshopFeature>(entity.Features).ToList();
            includedFeatures.ForEach(item => item.WebshopId = entity.Id);
            context.AddRange(includedFeatures);

            context.SaveChanges();
        }

        public void AddFeatures(Guid webshopId, IEnumerable<Guid> featureIds)
        {
            using (var context = CreateDataContext())
            {
                foreach (var featureId in featureIds)
                {
                    var dbEntity = new DbWebshopFeature
                    {
                        Id = Guid.NewGuid(),
                        WebshopId = webshopId,
                        FeatureId = featureId,
                        RemainingImplementationRequirements = Framework.Features.Get(featureId).ImplementationRequirements,
                        AddedOnIteration = Framework.Context.IterationNumber
                    };

                    context.Add(dbEntity);
                }
                context.SaveChanges();
            }
        }
    }
}
