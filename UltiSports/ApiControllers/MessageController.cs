using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using UltiSports.Models;
using UltiSports.Infrastructure;
using UltiSports.Services;
using Microsoft.AspNet.SignalR;

namespace UltiSports.ApiControllers
{
    public class MessageController : ApiController
    {
        private IMessageService _service;
        private IHubContext _hub;

        public MessageController(IMessageService service)
        {
            _service = service;
            // DI
            _hub = GlobalHost.ConnectionManager.GetHubContext<MessageHub>();
        }

        // GET api/Message
        public IEnumerable<Message> GetMessages()
        {
            return _service.GetAll().Data;
        }

        [Route("api/Message/Events/{eventId}")]
        public IEnumerable<Message> GetMessagesForEvent(int eventId)
        {
            return _service.GetMessagesForEvent(eventId).Data;
        }

        // GET api/Message/5
        public Message GetMessage(int id)
        {
            Message message = _service.GetByID(id).Data;
            if (message == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return message;
        }

        // PUT api/Message/5
        public HttpResponseMessage PutMessage(int id, Message message)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != message.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                _service.Update(message);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Message
        public HttpResponseMessage PostMessage(Message message)
        {
            if (ModelState.IsValid)
            {
                // We're not passing time from client to avoid time format issues
                message.Time = DateTime.Now;
                _service.Create(message.MessageText, message.Event.Id, User.Identity.Name);

                _hub.Clients.All.newChatMessage(message.Event.Id.ToString(), User.Identity.Name, message.MessageText);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, message);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = message.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Message/5
        public HttpResponseMessage DeleteMessage(int id)
        {
            Message message = _service.GetByID(id).Data;
            if (message == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            try
            {
                _service.Delete(message);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, message);
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            base.Dispose(disposing);
        }
    }
}