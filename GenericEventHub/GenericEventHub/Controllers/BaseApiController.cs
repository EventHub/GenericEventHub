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
    public class BaseApiController<TEntity, TEntityDTO> : ApiController 
        where TEntity : Entity
        where TEntityDTO : DTO
    {
        private IBaseService<TEntity> _service;
        protected DTOMapper<TEntity, TEntityDTO> _mapper;

        public BaseApiController(IBaseService<TEntity> service)
        {
            _service = service;
            _mapper = new DTOMapper<TEntity, TEntityDTO>();
        }

        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            var serviceResponse = _service.GetAll();

            HttpResponseMessage controllerResponse = null;
            if (serviceResponse.Success)
            {
                controllerResponse = Request.CreateResponse(HttpStatusCode.OK, _mapper.GetDTOForEntities(serviceResponse.Data));
            }
            else
                controllerResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, serviceResponse.Message);

            return controllerResponse;

        }

        [HttpGet]
        public HttpResponseMessage GetEntity(int id)
        {
            var entity = _service.GetByID(id).Data;
            if (entity == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return Request.CreateResponse(HttpStatusCode.OK, _mapper.GetDTOForEntity(entity));
        }

        [HttpPut]
        public HttpResponseMessage Put(int id, TEntity TEntity)
        {
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
        public HttpResponseMessage Post(TEntity entity)
        {
            if (ModelState.IsValid)
            {
                var res = _service.Create(entity);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, _mapper.GetDTOForEntity(entity));
                //response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = TEntity.TEntityID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
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
    }
}