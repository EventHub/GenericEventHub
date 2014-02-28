using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GenericEventHub.Models
{
    public class Activity
    {
        public int ActivityID { get; set; }
        [DisplayName("Activity Name")]
        public string Name { get; set; }

        [DisplayName("Day Of The Week")]
        public string DayOfWeek { get; set; }

        [DisplayName("Preferred Time Of Activity")]
        public DateTime PreferredTime { get; set; }
        public int LocationID { get; set; }

        // Navigation Properties

        public Location Location { get; set; }
    }
}