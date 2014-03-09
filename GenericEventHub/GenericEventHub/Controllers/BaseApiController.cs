using GenericEventHub.Authorization;
using GenericEventHub.DTOs;
using GenericEventHub.Models;
using GenericEventHub.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace GenericEventHub.Controllers
{
    [Authorize]
    public class BaseApiController<TEntity, TEntityDTO> : ApiController 
        where TEntity : Entity
        where TEntityDTO : DTO
    {
        private IBaseService<TEntity> _service;
        private IUserService _users;
        protected DTOMapper _mapper;

        public BaseApiController(IBaseService<TEntity> service,
            IUserService users)
        {
            _service = service;
            _users = users;
            _mapper = new DTOMapper();
        }

        [HttpGet]
        public virtual HttpResponseMessage GetAll()
        {
            var serviceResponse = _service.GetAll();

            HttpResponseMessage controllerResponse = null;
            if (serviceResponse.Success)
            {
                controllerResponse = Request.CreateResponse(HttpStatusCode.OK, _mapper.GetDTOsForEntities<TEntity, TEntityDTO>(serviceResponse.Data));
            }
            else
                controllerResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, serviceResponse.Message);

            return controllerResponse;

        }

        [HttpGet]
        public virtual HttpResponseMessage GetEntity(int id)
        {
            var entity = _service.GetByID(id).Data;
            if (entity == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return Request.CreateResponse(HttpStatusCode.OK, _mapper.GetDTOForEntity<TEntity, TEntityDTO>(entity));
        }

        [HttpPut]
        public virtual HttpResponseMessage Put(int id, TEntity TEntity)
        {
            if (!IsAdmin(User.Identity.Name))
                return Request.CreateResponse(HttpStatusCode.Forbidden);

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != TEntity.GetID())
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var response = _service.Update(TEntity);

            if (!response.Success)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, response.Message);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public virtual HttpResponseMessage Post(TEntity entity)
        {
            if (!IsAdmin(User.Identity.Name))
                return Request.CreateResponse(HttpStatusCode.Forbidden);

            if (ModelState.IsValid)
            {
                var res = _service.Create(entity);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, _mapper.GetDTOForEntity<TEntity, TEntityDTO>(entity));
                //response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = TEntity.TEntityID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpDelete]
        public virtual HttpResponseMessage Delete(int id)
        {
            if (!IsAdmin(User.Identity.Name))
                return Request.CreateResponse(HttpStatusCode.Forbidden);

            var entity = _service.GetByID(id).Data;
            if (entity == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var response = _service.Delete(entity);


            if (!response.Success)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, response.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, entity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _service.Dispose();
            }
            base.Dispose(disposing);
        }

        protected bool IsAdmin(string name)
        {
            var authorized = false;

            var user = _users.GetUserByWindowsName(name);

            if (user != null && user.Success)
            {
                authorized = user.Data.IsAdmin;
            } 

            return authorized;
        }
    }
}