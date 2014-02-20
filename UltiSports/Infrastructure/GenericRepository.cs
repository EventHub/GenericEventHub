using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using UltiSports.Models;

namespace UltiSports.Infrastructure
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal UltiEventsContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(UltiEventsContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == System.Data.Entity.EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            context.SaveChanges();
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public void UpdateActivity(Activity entityToUpdate)
        {
            var result = context.Activity.Single(act => act.Name == entityToUpdate.Name);
            result.PreferredLocation = context.Location.Single(loc => loc.Id == entityToUpdate.PreferredLocation.Id);

            result.DayOfTheWeek = entityToUpdate.DayOfTheWeek;
            result.RequiredNumberOfPlayers = entityToUpdate.RequiredNumberOfPlayers;
            result.RecommendedNumberOfPlayers = entityToUpdate.RecommendedNumberOfPlayers;
            result.PreferredTime = entityToUpdate.PreferredTime;

            context.Entry(result).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public void CreateActivity(Activity entityToCreate)
        {
            entityToCreate.PreferredLocation = context.Location.Single(loc => loc.Id == entityToCreate.PreferredLocation.Id);
            entityToCreate.IsActive = true;

            dbSet.Add(entityToCreate as TEntity);
            context.SaveChanges();
        }
    }

    public interface IGenericRepository<TEntity> : IDisposable
     where TEntity : class
    {
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        System.Collections.Generic.IEnumerable<TEntity> Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = null, Func<System.Linq.IQueryable<TEntity>, System.Linq.IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        TEntity GetByID(object id);
        void Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
        void UpdateActivity(Activity entityToUpdate);
        void CreateActivity(Activity entityToUpdate);
    }
}