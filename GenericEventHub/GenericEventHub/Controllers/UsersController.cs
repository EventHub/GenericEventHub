using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GenericEventHub.Models;
using GenericEventHub.Services;

namespace GenericEventHub.Controllers
{
    [RoutePrefix("Users")]
    public class UsersController : BaseApiController<User>
    {
        private IUserService _service;

        public UsersController(IUserService service) : base(service)
        {
            _service = service;
        }
        
        [Route("Current")]
        public HttpResponseMessage GetUserInformation()
        {
            var user = _service.GetUserByWindowsName(User.Identity.Name).Data;

            if (user == null)
            {
                // Create this user
                user = new User()
                {
                    WindowsName = User.Identity.Name,
                    IsAdmin = false
                };

                _service.Create(user);
            }

            return Request.CreateResponse(HttpStatusCode.OK, user);
        }
    }
}