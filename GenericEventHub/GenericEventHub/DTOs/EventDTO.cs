using GenericEventHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericEventHub.DTOs
{
    public class EventDTO : DTO
    {
        public int EventID { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public ActivityDTO Activity { get; set; }
        public ICollection<EventUserDTO> UsersInEvent { get; set; }
        public ICollection<EventGuestDTO> GuestsInEvent { get; set; }

        public EventDTO(Event ev) : base(ev)
        {
            Activity = new ActivityDTO(ev.Activity);

            //UsersInEvent = new List<EventUserDTO>();
            //GuestsInEvent = new List<EventGuestDTO>();
            //ev.UsersInEvent.ForEach(x => UsersInEvent.Add(new EventUserDTO(x)));
            //ev.GuestsInEvent.ForEach(x => GuestsInEvent.Add(new EventGuestDTO(x)));
        }
    }
}