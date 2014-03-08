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
    [RoutePrefix("api/Guests")]
    public class GuestsController : BaseApiController<Guest, GuestDTO>
    {
        private IGuestService _service;

        public GuestsController(IGuestService service,
            IUserService users)
            : base(service, users)
        {
            _service = service;
        }
    }
}