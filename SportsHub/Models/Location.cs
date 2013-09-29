using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportsHub.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual List<Event> Events { get; set; }
        public virtual List<Activity> Activities { get; set; }
    }
}