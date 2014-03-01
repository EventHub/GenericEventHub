using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GenericEventHub.Models;
using GenericEventHub.Services;

namespace GenericEventHub.Controllers
{
    [RoutePrefix("api/events")]
    public class EventController : ApiController
    {
        private IEventService _service;

        public EventController(IEventService service)
        {
            _service = service;
        }

        [Route("")]
        public HttpResponseMessage Get()
        {
            var serviceResponse = _service.GetAll();

            HttpResponseMessage controllerResponse = null;
            if (serviceResponse.Success)
                controllerResponse = Request.CreateResponse(HttpStatusCode.OK, serviceResponse.Data);
            else
                controllerResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, serviceResponse.Message);

            return controllerResponse;
        }

        [Route("{id:int}")]
        public Event Get(int id)
        {
            var ev = _service.GetByID(id).Data;
            if (ev == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return ev;
        }

        [Route("{id:int}")]
        public HttpResponseMessage PutEvent(int id, Event ev)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != ev.EventID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var response = _service.Update(ev);

            if (!response.Success)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, response.Message);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("")]
        public HttpResponseMessage PostEvent(Event ev)
        {
            if (ModelState.IsValid)
            {
                var res = _service.Create(ev);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, ev);
                //response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = ev.EventID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [Route("{id:int}")]
        public HttpResponseMessage DeleteEvent(int id)
        {
            var ev = _service.GetByID(id).Data;
            if (ev == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var response = _service.Delete(ev);


            if (!response.Success)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, response.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, ev);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _service.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}