using GenericEventHub.DTOs;
using GenericEventHub.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GenericEventHub.Hubs
{
    //[Authorize] Should we do this?
    public class ParticipantsHub : Hub
    {
        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            return base.OnDisconnected();
        }

        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }
    }

    public class Participants : IParticipants 
    {
        private IHubContext _context;

        public Participants(IHubContext context)
        {
            _context = context;
        }

        public void AddUser(EventUserDTO user, int eventId)
        {
            _context.Clients.All.addUser(user.Name, user.UserID, eventId.ToString());
        }

        public void RemoveUser(EventUserDTO user, int eventId)
        {
            _context.Clients.All.removeUser(user.Name, user.UserID, eventId.ToString());
        }

        public void AddGuest(EventGuestDTO guest)
        {
            _context.Clients.All.addGuest(guest.Name, guest.GuestID, guest.Host.Name, 
                guest.Host.UserID, guest.EventID.ToString());
        }

        public void RemoveGuest(EventGuestDTO guest)
        {
            _context.Clients.All.removeGuest(guest.Name, guest.GuestID, guest.Host.Name,
                guest.Host.UserID, guest.EventID.ToString());
        }
    }

    public interface IParticipants
    {
        void AddUser(EventUserDTO user, int eventId);
        void RemoveUser(EventUserDTO user, int eventId);
        void AddGuest(EventGuestDTO guest);
        void RemoveGuest(EventGuestDTO guest);
    }
}