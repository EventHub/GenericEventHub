using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UltiSports.Infrastructure;
using UltiSports.Models;
using UltiSports.Services;

namespace UltiSports.ApiControllers
{
    public class ParticipantsController : ApiController
    {
        IHubContext _hub;
        IAttendanceService _service;

        public ParticipantsController(IAttendanceService attendService,
            IPlusOneRepository guestRepo)
        {
            _service = attendService;
            _hub = GlobalHost.ConnectionManager.GetHubContext<ParticipantsHub>();
        }

        [Route("api/Events/Player")]
        public void PlayerJoined(Attendance attend)
        {
            _hub.Clients.All.playerJoined(attend);
        }

        [Route("api/Events/Guest")]
        [HttpPost]
        public void GuestJoined(PlusOne plusOne)
        {
            _hub.Clients.All.guestJoined(plusOne);
        }

        [Route("api/Events/Player")]
        [HttpDelete]
        public void PlayerLeft(Attendance attend)
        {
            _hub.Clients.All.playerLeft(attend);
        }

        [Route("api/Events/Guest")]
        [HttpDelete]
        public void GuestLeft(PlusOne plusOne)
        {
            _hub.Clients.All.guestLeft(plusOne);
        }
    }
}
