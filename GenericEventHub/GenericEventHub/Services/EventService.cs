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
    }

    public interface IEventService : IBaseService<Event>
    {

    }
}