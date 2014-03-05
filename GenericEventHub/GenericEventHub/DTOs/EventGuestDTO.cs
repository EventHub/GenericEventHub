using GenericEventHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericEventHub.DTOs
{
    public class EventGuestDTO : DTO
    {
        public int GuestID { get; set; }
        public string Name { get; set; }
        public int EventID { get; set; }
        public EventUserDTO Host { get; set; }

        public EventGuestDTO(Guest guest) : base(guest)
        {
            Host = new EventUserDTO(guest.Host);
        }
    }
}