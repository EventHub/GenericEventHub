using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GenericEventHub.Models
{
    public class Event
    {
        public int EventID { get; set; }
        [DisplayName("Event Name")]
        public string Name { get; set; }

        [Required]
        public int ActivityID { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        // Navigation Properties

        public virtual Activity Activity { get; set; }
        public virtual List<User> UsersInEvent { get; set; }
        public virtual List<Guest> GuestsInEvent { get; set; }

        // Methods

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