using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sana.Sim.EF.DbAccess;
using Sana.Sim.Business;
using Sana.Sim.EF.DbEntities;

namespace Sana.Sim.EF.Repositories
{
    public abstract class BaseRepository<TEntity, TDbEntity>
        where TDbEntity : DbEntity
    {
        private readonly DataContextFactory factory = IoC.Resolve<DataContextFactory>();

        public virtual TEntity Get(Guid id)
        {
            using (var context = CreateDataContext())
            {
                var entity = Mapper.Map<TEntity>(context.Set<TDbEntity>().SingleOrDefault(e => e.Id == id));
                PostLoad(context, entity);

                return entity;
            }
        }

        public virtual IEnumerable<TEntity> Get()
        {
            using (var context = CreateDataContext())
            {
                var entities = Map(context.Set<TDbEntity>().ToList()).ToList();
                entities.ForEach(e => PostLoad(context, e));

                return entities;
            }
        }

        public virtual void Create(TEntity entity)
        {
            using (var context = CreateDataContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var dbEntity = Mapper.Map<TDbEntity>(entity);
                    context.Add(dbEntity);
                    context.SaveChanges();

                    PostCreate(context, entity);

                    transaction.Commit();
                }
            }
        }

        public virtual void Update(TEntity entity)
        {
            using (var context = CreateDataContext())
            {
                var dbEntity = Mapper.Map<TDbEntity>(entity);
                context.Update(dbEntity);

                context.SaveChanges();
            }
        }

        public virtual void Delete(Guid id)
        {
            using (var context = CreateDataContext())
            {
                var dbEntity = context.Set<TDbEntity>().SingleOrDefault(e => e.Id == id);

                if (dbEntity != null)
                    context.Remove(dbEntity);

                context.SaveChanges();
            }
        }

        protected DataContext CreateDataContext() =>
            factory.CreateDataContext();

        protected virtual void PostLoad(DataContext context, TEntity entity)
        {
        }

        protected virtual void PostCreate(DataContext context, TEntity entity)
        {
        }

        protected IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> items) =>
            items.ToList().Select(item => Mapper.Map<TDestination>(item)).ToList();

        protected TDestination Map<TSource, TDestination>(TSource item) =>
            Mapper.Map<TDestination>(item);

        protected IEnumerable<TEntity> Map(IEnumerable<TDbEntity> items) =>
            Map<TDbEntity, TEntity>(items);
    }
}
