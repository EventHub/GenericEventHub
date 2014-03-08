using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GenericEventHub.Models;
using GenericEventHub.Services;
using GenericEventHub.DTOs;

namespace GenericEventHub.Controllers
{
    [Authorize]
    [RoutePrefix("api/Locations")]
    public class LocationsController : BaseApiController<Location, LocationDTO>
    {
        private ILocationService _service;

        public LocationsController(ILocationService service,
            IUserService users)
            : base(service, users)
        {
            _service = service;
        }

        
    }
}