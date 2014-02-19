using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using UltiSports.Models;
using UltiSports.Infrastructure;
using UltiSports.Services;
using Microsoft.AspNet.SignalR;

namespace UltiSports.ApiControllers
{
    public class EventsController : ApiController
    {
        private IEventService _service;
        private IHubContext _hub;

        public EventsController(IEventService service)
        {
            _service = service;
        }

        // GET api/Events
        public IEnumerable<Event> GetEvent()
        {
            return _service.GetAll().Data;
        }

        
        [Route("api/Events/{date:datetime}")]
        public IEnumerable<Event> GetEventsFor(DateTime date) {
            return _service.GetActiveEventsFor(date);
        }

        // GET api/Events/5
        [ResponseType(typeof(Event))]
        public IHttpActionResult GetEvent(int id)
        {
            Event @event = _service.GetByID(id).Data;
            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);
        }

        // PUT api/Events/5
        public IHttpActionResult PutEvent(int id, Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @event.Id)
            {
                return BadRequest();
            }

            try
            {
                _service.Update(@event);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Events
        [ResponseType(typeof(Event))]
        public IHttpActionResult PostEvent(Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _service.Create(@event);

            return CreatedAtRoute("DefaultApi", new { id = @event.Id }, @event);
        }

        // DELETE api/Events/5
        [ResponseType(typeof(Event))]
        public IHttpActionResult DeleteEvent(int id)
        {
            Event @event = _service.GetByID(id).Data;
            if (@event == null)
            {
                return NotFound();
            }

            _service.Delete(@event);

            return Ok(@event);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _service.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventExists(int id)
        {
            return _service.GetByID(id).Data != null;
        }
    }
}