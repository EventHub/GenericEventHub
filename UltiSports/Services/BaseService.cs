using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UltiSports.Infrastructure;

namespace UltiSports.Services
{
    public class BaseService<TEntity> : UltiSports.Services.IBaseService<TEntity> where TEntity : class
    {
        private IBaseRepository<TEntity> _repo;

        public BaseService(IBaseRepository<TEntity> repo) {
            _repo = repo;
        } 

        public ServiceResponse Create(TEntity entity) {
            string message = string.Empty;
            bool success = false;

            try
            {
                _repo.Insert(entity);
                message = String.Format("{0} successfully created", entity.ToString());
            }
            catch (Exception ex) {
                message = ex.Message;
            }

            return new ServiceResponse(message, success);
        }

        public ServiceResponse Delete(object id)
        {
            return Delete(_repo.GetByID(id));
        }

        public ServiceResponse Delete(TEntity entity)
        {
            string message = string.Empty;
            bool success = false;

            try
            {
                _repo.Delete(entity);
                message = String.Format("{0} successfully deleted", entity.ToString());
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return new ServiceResponse(message, success);
        }

        public virtual ServiceResponse Update(TEntity entity)
        {
            string message = string.Empty;
            bool success = false;

            try
            {
                _repo.Update(entity);
                message = String.Format("{0} successfully updated", entity.ToString());
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return new ServiceResponse(message, success);
        }

        public ServiceData<TEntity> GetByID(object id)
        {
            string message = string.Empty;
            bool success = false;
            TEntity data = null;

            try
            {
                data = _repo.GetByID(id);
                message = String.Format("{0} successfully obtained", data.ToString());
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return new ServiceData<TEntity>(data, message, success);
        }

        public ServiceData<IEnumerable<TEntity>> GetAll()
        {
            string message = string.Empty;
            bool success = false;
            IEnumerable<TEntity> data = null;

            try
            {
                data = _repo.GetAll();
                message = String.Format("All successfully obtained", data.ToString());
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return new ServiceData<IEnumerable<TEntity>>(data, message, success);
        }

        public void Dispose()
        {
            _repo.Dispose();
        }
    }

    public interface IBaseService<TEntity> : IDisposable
     where TEntity : class
    {
        ServiceResponse Create(TEntity entity);
        ServiceResponse Delete(object id);
        ServiceResponse Delete(TEntity entity);
        ServiceData<System.Collections.Generic.IEnumerable<TEntity>> GetAll();
        ServiceData<TEntity> GetByID(object id);
        ServiceResponse Update(TEntity entity);
    }
}