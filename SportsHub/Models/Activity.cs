using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportsHub.Models
{
    public class Activity
    {
        [Key]
        public string Name { get; set; }
        public DateTime PreferredTime { get; set; }
        public string DayOfTheWeek { get; set; }
        public int RequiredNumberOfPlayers { get; set; }
        public int RecommendedNumberOfPlayers { get; set; }
        public Location PreferredLocation { get; set; }
        public List<Player> Managers { get; set; }
        public virtual List<Event> Events { get; set; }
    }
}