using SportsHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SportsHub.Infrastructure
{
    public class ActivityDb : DatabaseServices
    {
        public List<Activity> GetActivitiesOfTheDay() 
        {
            string today = DateTime.Today.DayOfWeek.ToString();
            return _Db.Activity.Where(x => x.DayOfTheWeek.Equals(today)).ToList();
        }

        public bool IsTodayActivityDay() 
        {
            return GetActivitiesOfTheDay().Count > 0;
        }
    }
}