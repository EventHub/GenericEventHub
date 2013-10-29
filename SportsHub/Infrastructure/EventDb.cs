using SportsHub.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;

namespace SportsHub.Infrastructure
{
    public class EventDb : DatabaseServices
    {
        public List<Event> GetEventsOfTheDay(List<Activity> activitiesOfTheDay, Player user) 
        {
            List<Event> todaysEvents = new List<Event>();

            foreach (Activity activity in activitiesOfTheDay) 
            {
                bool eventExists = false;

                try
                {
                    foreach (Event ev in activity.Events)
                    {
                        if (ev.Time.Date == DateTime.Today.Date)
                        {
                            todaysEvents.Add(ev);
                            eventExists = true;
                        }
                    }

                    if (!eventExists)
                    {
                        todaysEvents.Add(CreateEventOfTheDay(activity));
                    }
                }
                catch (Exception)
                {
                    todaysEvents.Add(CreateEventOfTheDay(activity));
                }
            }

            PreferredEventSorter sorter = new PreferredEventSorter(user);
            todaysEvents.Sort(sorter);
            return todaysEvents;
        }

        public Event CreateEventOfTheDay(Activity activity) 
        {
            _Db.Activity.Attach(activity);

            Event eventToAdd = new Event()
            {
                Activity = activity,
                Attendees = new List<Attendance>(),
                Location = activity.PreferredLocation,
                Messages = new List<Message>(),
                PlusOnes = new List<PlusOne>(),
                Time = Convert.ToDateTime(activity.PreferredTime.ToString()),
            };

            AddEvent(eventToAdd);
            return eventToAdd;
        }

        /// <summary>
        /// Returns a single event that has a specified Id.
        /// </summary>
        /// <param name ="id">The id of an event which is also the primary key in the Db.</param>
        internal Event GetEventById(int id)
        {
            return _Db.Event.SingleOrDefault(ev => ev.Id == id);
        }

        internal bool IsInAnExistingEvent(Activity activityToDelete)
        {
            var allEvents = _Db.Event;
            var result = Enumerable.Any(allEvents, ev => ev.Activity == activityToDelete);

            return result;
        }

        public List<Event> AllEvents
        {
            get { return _Db.Event.ToList(); }
        }
    }

    public class PreferredEventSorter : IComparer<Event>
    {
        private AttendanceDb _attendanceDb = new AttendanceDb();
        private Player user;

        public PreferredEventSorter(Player user)
        {
            this.user = user;
        }

        public int Compare(Event a, Event b)
        {
            var eventA = (Event)a;
            var eventB = (Event)b;

            int eventAattendanceCount = this._attendanceDb.GetPlayerAttendanceForActivity(eventA.Activity, user, DateTime.Today.AddMonths(-6));
            int eventBattendanceCount = this._attendanceDb.GetPlayerAttendanceForActivity(eventB.Activity, user, DateTime.Today.AddMonths(-6));

            if (eventAattendanceCount > eventBattendanceCount) return -1;
            if (eventAattendanceCount < eventBattendanceCount) return 1;
            return 0;
        }
    }
}