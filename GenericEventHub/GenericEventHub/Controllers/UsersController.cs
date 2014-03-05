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
    [RoutePrefix("Users")]
    public class UsersController : BaseApiController<User, UserDTO>
    {
        private IUserService _service;

        public UsersController(IUserService service) : base(service)
        {
            _service = service;
        }
        
        [HttpGet]
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

            return Request.CreateResponse(HttpStatusCode.OK, base._mapper.GetDTOForEntity(user));
        }

        [HttpPost]
        [Route("Name")]
        public HttpResponseMessage UpdateUserName(int id, string name)
        {
            var user = _service.GetUserByWindowsName(User.Identity.Name).Data;

            if (user == null)
            {
                if (user.UserID != id)
                    return Request.CreateResponse(HttpStatusCode.Unauthorized);

                // Create this user
                user.Name = name;

                _service.Update(user);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}