using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GenericEventHub.Models;
using GenericEventHub.Services;

namespace GenericEventHub.Controllers
{
    [RoutePrefix("api/locations")]
    public class LocationController : ApiController
    {
        private ILocationService _service;

        public LocationController(ILocationService service)
        {
            _service = service;
        }

        [Route("")]
        public IEnumerable<Location> Get()
        {
            return _service.GetAll().Data;
        }

        [Route("{id:int}")]
        public Location Get(int id)
        {
            var location = _service.GetByID(id).Data;
            if (location == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return location;
        }

        [Route("{id:int}")]
        public HttpResponseMessage PutLocation(int id, Location location)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != location.LocationID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var response = _service.Update(location);

            if (!response.Success)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, response.Message);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("")]
        public HttpResponseMessage PostLocation(Location location)
        {
            if (ModelState.IsValid)
            {
                var res = _service.Create(location);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, location);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = location.LocationID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [Route("{id:int}")]
        public HttpResponseMessage DeleteLocation(int id)
        {
            var location = _service.GetByID(id).Data;
            if (location == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var response = _service.Delete(location);


            if (!response.Success)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, response.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, location);
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