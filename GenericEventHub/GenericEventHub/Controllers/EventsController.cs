using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GenericEventHub.Models;
using GenericEventHub.Services;
using Microsoft.AspNet.SignalR;
using GenericEventHub.Hubs;
using GenericEventHub.DTOs;

namespace GenericEventHub.Controllers
{
    [System.Web.Http.Authorize]
    [RoutePrefix("api/Events")]
    public class EventsController : BaseApiController<Event, EventDTO>
    {
        private IEventService _service;
        private IUserService _userService;
        private IGuestService _guestService;
        private IParticipants _participantsContext;

        public EventsController(IEventService service,
            IUserService userService,
            IGuestService guestService) : base(service, userService)
        {
            _service = service;
            _userService = userService;
            _guestService = guestService;
            _participantsContext = new Participants(GlobalHost.ConnectionManager.GetHubContext<ParticipantsHub>());
        }

        [HttpGet]
        [Route("{date:datetime}")]
        public HttpResponseMessage GetEventsForDate(DateTime date)
        {
            var serviceResponse = _service.GetEventsAfter(date);
            if (serviceResponse.Success)
                return Request.CreateResponse(HttpStatusCode.OK, serviceResponse.Data);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "");
        }

        [HttpPost]
        [Route("{eventID:int}/AddUser")]
        public HttpResponseMessage AddUser(int eventID)
        {
            var user = _userService.GetUserByWindowsName(User.Identity.Name).Data;

            if (user == null)
                return Request.CreateResponse(HttpStatusCode.Forbidden);

            var ev = _service.GetByID(eventID).Data;

            if (ev != null && !ev.UsersInEvent.Contains(user))
            {
                ev.UsersInEvent.Add(user); 
                _service.Update(ev);
                _participantsContext.AddUser(new EventUserDTO(user), eventID);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("{eventID:int}/RemoveUser")]
        public HttpResponseMessage RemoveUser(int eventID)
        {
            var user = _userService.GetUserByWindowsName(User.Identity.Name).Data;

            if (user == null)
                return Request.CreateResponse(HttpStatusCode.Forbidden);

            // Check for null event
            var ev = _service.GetByID(eventID).Data;

            if (ev != null && ev.UsersInEvent.Contains(user))
            {
                ev.UsersInEvent.Remove(user);
                _service.Update(ev);
                _participantsContext.RemoveUser(new EventUserDTO(user), eventID);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("{eventID:int}/AddGuest")]
        public HttpResponseMessage AddGuest(int eventID, Guest guest)
        {
            var host = _userService.GetUserByWindowsName(User.Identity.Name).Data;

            if (host == null)
                return Request.CreateResponse(HttpStatusCode.Forbidden);

            guest.HostID = host.UserID;
            _guestService.Create(guest);
            var ev = _service.GetByID(eventID).Data;

            guest.Event = ev;
            guest.Host = host;
            if (ev != null)
            {
                ev.GuestsInEvent.Add(guest);
                _service.Update(ev);
                _participantsContext.AddGuest(new EventGuestDTO(guest));
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("{eventID:int}/RemoveGuest/{guestID:int}")]
        public HttpResponseMessage RemoveGuest(int eventID, int guestID)
        {
            var guest = _guestService.GetByID(guestID).Data;

            var user = guest.Host;
            if (user != null && !user.WindowsName.Equals(user.WindowsName))
                return Request.CreateResponse(HttpStatusCode.Forbidden);

            var ev = _service.GetByID(eventID).Data;
            //guest.HostID = -1;
            //guest.Host = null;

            if (ev != null && ev.GuestsInEvent.Contains(guest))
            {
                //ev.GuestsInEvent.Remove(guest);
                //_service.Update(ev);
                _participantsContext.RemoveGuest(new EventGuestDTO(guest));
                _guestService.Delete(guest);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}