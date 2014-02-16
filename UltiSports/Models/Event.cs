using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Services.Description;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace UltiSports.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public DateTime Time { get; set; }
        [ForeignKey("Activity")]
        public string ActivityName { get; set; }
        public virtual Activity Activity { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
        public virtual List<Player> Attendees { get; set; }
        public virtual List<PlusOne> PlusOnes { get; set; }
        public virtual List<Message> Messages { get; set; }
        [Display(Name = "Canceled")]
        public bool IsCanceled { get; set; }
    }
}