using GenericEventHub.Models;
using GenericEventHub.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventGenerator
{
    public class EventGenerator
    {
        private IEventRepository _eventRepo;
        private IActivityRepository _activityRepo;

        public EventGenerator(IEventRepository eventRepo,
            IActivityRepository activityRepo)
        {
            _eventRepo = eventRepo;
            _activityRepo = activityRepo;
        }

        public void CreateEventsForNextWeek()
        {
            var today = DateTime.Today;
            var todayPlusSix = today.AddDays(6);

            CreateEventsFor(today, todayPlusSix);
        }

        public void CreateEventsFor(DateTime start, DateTime end)
        {
            var activities = _activityRepo.GetAll();

            CreateEventsForActivitiesAndDateRange(activities, start, end);
        }

        public void CreateEventsForActivitiesAndDateRange(IEnumerable<Activity> activities, DateTime startDate, DateTime endDate)
        {
            var events = GenerateEventsForActivitiesAndDateRange(activities, startDate, endDate);

            events.ForEach(ev => _eventRepo.Insert(ev));
        }

        public List<Event> GenerateEventsForActivitiesAndDateRange(IEnumerable<Activity> activities, DateTime startDate, DateTime endDate)
        {
            var dates = DatesInRange(startDate, endDate);
            var events = new List<Event>();

            Event ev;
            DateTime time;
            foreach (var date in dates)
            {
                foreach (var activity in activities)
                {
                    if (activity.DayOfWeek.Equals(date.DayOfWeek.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        // Check to see if an event already exists for this day
                        if (DoesEventExistForActivityAndDate(activity, date))
                            continue;

                        time = new DateTime(
                            date.Year,
                            date.Month,
                            date.Day,
                            activity.PreferredTime.Hours,
                            activity.PreferredTime.Minutes,
                            activity.PreferredTime.Seconds,
                            activity.PreferredTime.Milliseconds,
                            date.Kind);
                        ev = new Event
                        {
                            Name = activity.Name,
                            ActivityID = activity.ActivityID,
                            DateTime = time
                        };

                        events.Add(ev);
                    }
                }
            }
            return events;
        }

        public IEnumerable<DateTime> DatesInRange(DateTime start, DateTime end)
        {
            var dates = new List<DateTime>();

            for (var dt = start; dt <= end; dt = dt.AddDays(1))
            {
                dates.Add(dt);
            }

            return dates;
        }

        public bool DoesEventExistForActivityAndDate(Activity activity, DateTime date)
        {
            var exists = _eventRepo.GetEventsOn(date.Date);

            return (exists.Where<Event>(x => x.ActivityID.Equals(activity.ActivityID)).Count() > 0);
        }
    }
}
