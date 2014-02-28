using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GenericEventHub.Models
{
    public class Guest
    {
        public int GuestID { get; set; }
        [DisplayName("Guest Name")]
        public string Name { get; set; }

        public int HostID { get; set; }
        public int EventID { get; set; }

        // Navigation Properties
        public User Host { get; set; }
        public Event Event { get; set; }
    }
}