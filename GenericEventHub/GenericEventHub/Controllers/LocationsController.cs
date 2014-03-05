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
    [RoutePrefix("locations")]
    public class LocationsController : BaseApiController<Location, LocationDTO>
    {
        private ILocationService _service;

        public LocationsController(ILocationService service) : base(service)
        {
            _service = service;
        }

        
    }
}