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
        public virtual User Host { get; set; }
        public virtual Event Event { get; set; }
    }
}