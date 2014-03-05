using GenericEventHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericEventHub.DTOs
{
    public class ActivityDTO : DTO
    {
        public int ActivityID { get; set; }
        public string Name { get; set; }
        public string DayOfWeek { get; set; }
        public TimeSpan PreferredTime { get; set; }
        public LocationDTO Location { get; set; }

        public ActivityDTO(Activity activity) : base(activity)
        {
            Location = new LocationDTO(activity.Location);
        }
    }
}