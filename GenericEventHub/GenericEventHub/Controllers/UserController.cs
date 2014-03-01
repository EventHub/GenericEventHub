using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GenericEventHub.Models;
using GenericEventHub.Services;

namespace GenericEventHub.Controllers
{
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        private IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
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