using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Services.Description;

namespace SportsHub.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public Activity ActivityName { get; set; }
        public Location Location { get; set; }
        public List<Attendance> Attendees { get; set; }
        public List<Message> Messages { get; set; } 
    }
}