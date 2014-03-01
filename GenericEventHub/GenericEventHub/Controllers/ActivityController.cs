using GenericEventHub.Models;
using GenericEventHub.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GenericEventHub.Controllers
{
    [RoutePrefix("api/activities")]
    public class ActivityController : ApiController
    {
        private IActivityService _service;

        public ActivityController(IActivityService service)
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
        public Activity Get(int id)
        {
            var activity = _service.GetByID(id).Data;
            if (activity == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return activity;
        }

        [Route("{id:int}")]
        public HttpResponseMessage PutActivity(int id, Activity activity)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != activity.ActivityID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var response = _service.Update(activity);

            if (!response.Success)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, response.Message);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("")]
        public HttpResponseMessage PostActivity(Activity activity)
        {
            if (ModelState.IsValid)
            {
                var res = _service.Create(activity);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, activity);
                //response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = activity.ActivityID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [Route("{id:int}")]
        public HttpResponseMessage DeleteActivity(int id)
        {
            var activity = _service.GetByID(id).Data;
            if (activity == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var response = _service.Delete(activity);


            if (!response.Success)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, response.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, activity);
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