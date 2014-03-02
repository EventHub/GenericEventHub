using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GenericEventHub.Models;
using GenericEventHub.Services;

namespace GenericEventHub.Controllers
{
    [Authorize]
    public class EventsController : BaseApiController<Event>
    {
        private IEventService _service;

        public EventsController(IEventService service) : base(service)
        {
            _service = service;
        }

    }
}