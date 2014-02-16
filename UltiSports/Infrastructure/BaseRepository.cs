using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UltiSports.Infrastructure
{
    public class BaseRepository<TEntity> where TEntity : class
    {
        protected IGenericRepository<TEntity> _repo;

        public BaseRepository(IGenericRepository<TEntity> repo)
        {
            _repo = repo;
        }

        public void Insert(TEntity entity) {
            _repo.Insert(entity);
        }

        public void Update(TEntity entity)
        {
            _repo.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _repo.Delete(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _repo.Get();
        }

        public TEntity GetByID(object id)
        {
            return _repo.GetByID(id);
        }

        public void Dispose()
        {
            _repo.Dispose();
        }
    }
        public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
        {
            void Insert(TEntity entity);
            void Update(TEntity entity);
            void Delete(TEntity entity);
            TEntity GetByID(object id);
            IEnumerable<TEntity> GetAll();
        }
}