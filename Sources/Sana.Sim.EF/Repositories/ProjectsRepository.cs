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
    public class ProjectsRepository : BaseRepository<Project, DbProject>, IProjectsRepository
    {
        public void AddDeveloper(Guid projectId, Guid developerId)
        {
            using (var context = CreateDataContext())
            {
                var dbEntity = new DbProjectDeveloper
                {
                    Id = Guid.NewGuid(),
                    ProjectId = projectId,
                    DeveloperId = developerId
                };

                context.Add(dbEntity);

                context.SaveChanges();
            }
        }

        public void AddServer(Guid projectId, Guid serverId)
        {
            using (var context = CreateDataContext())
            {
                var dbEntity = new DbProjectServer
                {
                    Id = Guid.NewGuid(),
                    ProjectId = projectId,
                    ServerId = serverId
                };

                context.Add(dbEntity);

                context.SaveChanges();
            }
        }

        public void SaveIterationData(Project project, int iterationNumber)
        {
            var projectState = project.Changes.Single(s => s.IterationNumber == iterationNumber);
            var webshopStates = project.Webshops.Select(w => new
            {
                WebshopId = w.Id,
                State = w.Changes.Single(s => s.IterationNumber == iterationNumber)
            });

            var webshopFeatures = project.Webshops.Select(w => new
            {
                WebshopId = w.Id,
                Features = w.Features.Where(f => f.LastUpdatedIteration == iterationNumber)
            });

            using (var context = CreateDataContext())
            {
                var dbProjectState = Mapper.Map<DbProjectChangeSet>(projectState);
                dbProjectState.ProjectId = project.Id;
                context.Add(dbProjectState);

                foreach (var item in webshopStates)
                {
                    var dbWebshopState = Mapper.Map<DbWebshopChangeSet>(item.State);
                    dbWebshopState.WebshopId = item.WebshopId;
                    context.Add(dbWebshopState);
                }

                foreach (var item in webshopFeatures)
                {
                    var dbWebshopFeatures = Map<WebshopFeature, DbWebshopFeature>(item.Features.Where(f => !f.Deleted)).ToList();
                    dbWebshopFeatures.ForEach(f => f.WebshopId = item.WebshopId);
                    context.UpdateRange(dbWebshopFeatures);

                    var dbWebshopFeaturesToDelete = Map<WebshopFeature, DbWebshopFeature>(item.Features.Where(f => f.Deleted)).ToList();
                    dbWebshopFeaturesToDelete.ForEach(f => f.WebshopId = item.WebshopId);
                    context.RemoveRange(dbWebshopFeaturesToDelete);
                }

                context.SaveChanges();
            }
        }

        protected override void PostCreate(DataContext context, Project entity)
        {
            var states = Map<ProjectChangeSet, DbProjectChangeSet>(entity.Changes).ToList();
            states.ForEach(item => item.ProjectId = entity.Id);
            context.AddRange(states);

            var developers = Map<ProjectDeveloper, DbProjectDeveloper>(entity.Developers).ToList();
            developers.ForEach(item => item.ProjectId = entity.Id);
            context.AddRange(developers);

            var servers = Map<ProjectServer, DbProjectServer>(entity.Servers).ToList();
            servers.ForEach(item => item.ProjectId = entity.Id);
            context.AddRange(servers);

            context.SaveChanges();
        }

        protected override void PostLoad(DataContext context, Project entity)
        {
            if (entity == null)
                return;

            var stateQuery = context.Set<DbProjectChangeSet>().Where(s => s.ProjectId == entity.Id).OrderBy(s => s.IterationNumber);
            entity.Changes = Map<DbProjectChangeSet, ProjectChangeSet>(stateQuery);

            entity.Webshops = Framework.Webshops.GetForProject(entity.Id);

            var developersQuery = context.Set<DbProjectDeveloper>().Where(s => s.ProjectId == entity.Id);
            entity.Developers = Map<DbProjectDeveloper, ProjectDeveloper>(developersQuery);

            var serversQuery = context.Set<DbProjectServer>().Where(s => s.ProjectId == entity.Id);
            entity.Servers = Map<DbProjectServer, ProjectServer>(serversQuery);
        }
    }
}
