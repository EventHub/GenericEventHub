using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GenericEventHub.Models
{
    public class Event
    {
        public int EventID { get; set; }
        [DisplayName("Event Name")]
        public string Name { get; set; }

        public int ActivityID { get; set; }
        public List<int> UsersInEventID { get; set; }

        // Navigation Properties

        public Activity Activity { get; set; }
        public List<User> UsersInEvent { get; set; }
        public List<Guest> GuestsInEvent { get; set; }

        // Methods

        public DateTime GetTime()
        {
            return Activity.PreferredTime;
        }

        public string GetName()
        {
            return Activity.Name;
        }

        public Location GetLocation()
        {
            return Activity.Location;
        }
    }
}