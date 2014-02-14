using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UltiSports.Infrastructure;
using UltiSports.Models;

namespace UltiSports.Services
{
    public class EventService : BaseService<Event>, IEventService
    {
        private IEventRepository _eventDb;
        private IAttendanceRepository _attendanceDb;

        public EventService(IEventRepository eventDb, IAttendanceRepository attendanceDb) : base(eventDb)
        {
            _eventDb = eventDb;
            _attendanceDb = attendanceDb;
        }

        public List<Event> GetEventsOfTheDay(IEnumerable<Activity> activitiesOfTheDay, Player user)
        {
            List<Event> todaysEvents = new List<Event>();

            foreach (Activity activity in activitiesOfTheDay)
            {
                bool eventExists = false;

                try
                {
                    foreach (Event ev in activity.Events.Where(ev => ev.Time.Date == DateTime.Today.Date))
                    {
                        todaysEvents.Add(ev);
                        eventExists = true;
                    }

                    if (!eventExists)
                    {
                        todaysEvents.Add(_eventDb.CreateEventOfTheDay(activity));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            var sorter = new PreferredEventSorter(user, _attendanceDb);
            todaysEvents.Sort(sorter);
            return todaysEvents;
        }
    }

    public interface IEventService : IBaseService<Event>
    {
        System.Collections.Generic.List<UltiSports.Models.Event> GetEventsOfTheDay(System.Collections.Generic.IEnumerable<UltiSports.Models.Activity> activitiesOfTheDay, UltiSports.Models.Player user);
    }

    public class PreferredEventSorter : IComparer<Event>
    {
        private IAttendanceRepository _attendanceDb;
        private Player _user;

        public PreferredEventSorter(Player user, IAttendanceRepository attendanceDb)
        {
            _user = user;
            _attendanceDb = attendanceDb;
        }

        public int Compare(Event a, Event b)
        {
            var eventA = a;
            var eventB = b;

            var eventAattendanceCount = _attendanceDb.GetPlayerAttendanceForActivity(eventA.Activity, _user, DateTime.Today.AddMonths(-6));
            var eventBattendanceCount = _attendanceDb.GetPlayerAttendanceForActivity(eventB.Activity, _user, DateTime.Today.AddMonths(-6));

            if (eventAattendanceCount > eventBattendanceCount) return -1;
            if (eventAattendanceCount < eventBattendanceCount) return 1;
            return 0;
        }
    }
}