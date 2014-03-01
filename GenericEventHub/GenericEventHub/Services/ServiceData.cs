using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericEventHub.Services
{
    public class ServiceData<TEntity> : ServiceResponse where TEntity : class
    {
        private TEntity _data;

        public ServiceData(TEntity data, string message, bool success)
            : base(message, success)
        {
            _data = data;
        }

        public TEntity Data { get { return _data; } }
    }
}