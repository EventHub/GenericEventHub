using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericEventHub.DTOs
{
    public class EventGuestDTO
    {
        public int GuestID { get; set; }
        public string Name { get; set; }
        public int EventId { get; set; }
    }
}