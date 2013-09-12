using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportsHub.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public Activity ActivityName { get; set; }
        public Location Location { get; set; }
        public virtual List<Attendance> Attendees { get; set; }
        public virtual List<Message> Messages { get; set; } 
    }
}