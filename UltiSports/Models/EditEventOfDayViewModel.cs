using System.Collections.Generic;

namespace UltiSports.Models
{
    public class EditEventOfDayViewModel
    {
        public List<Location> AllLocations { get; set; }
        public List<Activity> AllActivities { get; set; }
        public Event EventOfTheDay { get; set; }
    }
}