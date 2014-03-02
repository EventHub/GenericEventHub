using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GenericEventHub.Models;
using GenericEventHub.Services;

namespace GenericEventHub.Controllers
{
    [RoutePrefix("guests")]
    public class GuestController : ApiController
    {
        private IGuestService _service;

        public GuestController(IGuestService service)
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
        public Guest Get(int id)
        {
            var guest = _service.GetByID(id).Data;
            if (guest == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return guest;
        }

        [Route("{id:int}")]
        public HttpResponseMessage PutGuest(int id, Guest guest)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != guest.GuestID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var response = _service.Update(guest);

            if (!response.Success)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, response.Message);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("")]
        public HttpResponseMessage PostGuest(Guest guest)
        {
            if (ModelState.IsValid)
            {
                var res = _service.Create(guest);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, guest);
                //response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = guest.GuestID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [Route("{id:int}")]
        public HttpResponseMessage DeleteGuest(int id)
        {
            var guest = _service.GetByID(id).Data;
            if (guest == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var response = _service.Delete(guest);


            if (!response.Success)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, response.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, guest);
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