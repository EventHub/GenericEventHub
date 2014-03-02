using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericEventHub.Models
{
    public class User : Entity
    {
        public int UserID { get; set; }
        public string WindowsName { get; set; }
        public string Name { get; set; }
        public string EMail { get; set; }

        public bool IsAdmin { get; set; }
        public virtual List<Event> EventsAttended { get; set; }

        public override int GetID()
        {
            return UserID;
        }
    }
}