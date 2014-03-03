using GenericEventHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericEventHub.DTOs
{
    public class EventDTO
    {
        public int EventID { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public Activity Activity { get; set; }
        public ICollection<EventUserDTO> UsersInEvent { get; set; }
        public ICollection<Guest> GuestsInEvent { get; set; }

        //public EventDTO(Event ev)
        //{
        //    EventID = ev.EventID;
        //    Name = ev.Name;
        //    DateTime = ev.DateTime;
        //    Activity = ev.Activity;

        //    ev.UsersInEvent.ForEach(x => UsersInEvent.Add(new EventUserDTO(x)));
        //}
    }
}