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
    [RoutePrefix("guests")]
    public class GuestsController : BaseApiController<Guest>
    {
        private IGuestService _service;

        public GuestsController(IGuestService service) : base(service)
        {
            _service = service;
        }
    }
}