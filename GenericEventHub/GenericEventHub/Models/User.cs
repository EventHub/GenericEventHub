using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericEventHub.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string EMail { get; set; }
        public virtual List<Event> EventsAttended { get; set; }
    }
}