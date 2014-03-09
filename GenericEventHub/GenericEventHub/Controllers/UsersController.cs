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
    [RoutePrefix("api/Users")]
    public class UsersController : BaseApiController<User, UserDTO>
    {
        private IUserService _service;

        public UsersController(IUserService service) : base(service, service)
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

            return Request.CreateResponse(HttpStatusCode.OK, base._mapper.GetDTOForEntity<User, UserDTO>(user));
        }

        [Route("Current")]
        public HttpResponseMessage PostUserName(User reqUser)
        {
            if (reqUser == null) {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var user = _service.GetUserByWindowsName(User.Identity.Name).Data;
            if (user != null)
            {
                if (user.UserID != reqUser.UserID)
                    return Request.CreateResponse(HttpStatusCode.Forbidden);

                if (reqUser.Name != null)
                    user.Name = reqUser.Name;

                if (reqUser.Email != null)
                    user.Email = reqUser.Email;

                _service.Update(user);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}