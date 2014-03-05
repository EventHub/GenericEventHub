using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GenericEventHub.Models;
using GenericEventHub.Repositories;

namespace GenericEventHub.Services
{
    public class EventService : BaseService<Event>, IEventService
    {
        private IEventRepository _repo;

        public EventService(IEventRepository repo)
            : base(repo)
        {
            _repo = repo;
        }

        public ServiceData<IEnumerable<Event>> GetEventsAfter(DateTime date)
        {
            var success = false;
            var message = "";
            IEnumerable<Event> data = null;

            try
            {
                data = _repo.GetEventsAfter(date);
                success = true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return new ServiceData<IEnumerable<Event>>(data, message, success);
        }
    }


    public interface IEventService : IBaseService<Event>
    {
        ServiceData<IEnumerable<Event>> GetEventsAfter(DateTime date);
    }
}