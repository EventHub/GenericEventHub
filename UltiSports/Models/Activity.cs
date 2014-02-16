using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UltiSports.Models
{
    public class Activity
    {
        [Key]
        public string Name { get; set; }
        [Display(Name = "Time")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan PreferredTime { get; set; }
        [Display(Name = "Day")]
        public string DayOfTheWeek { get; set; }
        [Display(Name = "Required Players")]
        public int RequiredNumberOfPlayers { get; set; }
        [Display(Name = "Recomended Players")]
        public int RecommendedNumberOfPlayers { get; set; }
        [Display(Name = "Location")]
        public virtual Location PreferredLocation { get; set; }
        public virtual List<Player> Managers { get; set; }
        public virtual List<Event> Events { get; set; }
        public bool IsActive { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}