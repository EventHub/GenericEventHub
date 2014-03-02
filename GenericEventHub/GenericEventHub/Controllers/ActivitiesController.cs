using GenericEventHub.Models;
using GenericEventHub.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GenericEventHub.Controllers
{
    [Authorize]
    [RoutePrefix("activities")]
    public class ActivitiesController : BaseApiController<Activity>
    {
        private IActivityService _service;

        public ActivitiesController(IActivityService service) : base(service)
        {
            _service = service;
        }
    }
}