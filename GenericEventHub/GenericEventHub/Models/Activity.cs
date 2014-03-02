using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GenericEventHub.Models
{
    public class Activity : Entity
    {
        public int ActivityID { get; set; }
        [Required]
        [DisplayName("Activity Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Day Of The Week")]
        public string DayOfWeek { get; set; }
        [Required]
        [DisplayName("Preferred Time Of Activity")]
        public TimeSpan PreferredTime { get; set; }
        [Required]
        public int LocationID { get; set; }

        // Navigation Properties

        public virtual Location Location { get; set; }

        public override int GetID()
        {
            return ActivityID;
        }
    }
}