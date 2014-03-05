using GenericEventHub.DTOs;
using GenericEventHub.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericEventHub.Hubs
{
    //[Authorize] Should we do this?
    public class ParticipantsHub : Hub
    {

    }

    public class Participants : IParticipants 
    {
        private IHubContext _context;

        public Participants(IHubContext context)
        {
            _context = context;
        } 

        public void AddUser(EventUserDTO user)
        {
            _context.Clients.All.addUser(user.Name, user.UserID, string.Empty, string.Empty);
        }

        public void RemoveUser(EventUserDTO user)
        {
            _context.Clients.All.removeUser(user.Name, user.UserID, string.Empty, string.Empty);
        }

        public void AddGuest(EventGuestDTO guest)
        {
            _context.Clients.All.addGuest(guest.Name, guest.GuestID, guest.Host.Name, guest.Host.UserID);
        }

        public void RemoveGuest(EventGuestDTO guest)
        {
            _context.Clients.All.removeGuest(guest.Name, guest.GuestID, guest.Host.Name, guest.Host.UserID);
        }
    }

    public interface IParticipants
    {
        void AddUser(EventUserDTO user);
        void RemoveUser(EventUserDTO user);
        void AddGuest(EventGuestDTO guest);
        void RemoveGuest(EventGuestDTO guest);
    }
}