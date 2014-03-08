using GenericEventHub.DTOs;
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
    [RoutePrefix("api/Activities")]
    public class ActivitiesController : BaseApiController<Activity, ActivityDTO>
    {
        private IActivityService _service;

        public ActivitiesController(IActivityService service,
            IUserService users) : base(service, users)
        {
            _service = service;
        }
    }
}