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
        public List<Event> GetEventsOfTheDay(List<Activity> activitiesOfTheDay) 
        {
            List<Event> todaysEvents = new List<Event>();

            foreach (Activity activity in activitiesOfTheDay) 
            {
                bool eventExists = false;

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

            //PreferredEventSorter sorter = new PreferredEventSorter(username);
            //todaysEvents.Sort(sorter);
            return todaysEvents;
        }

        public Event CreateEventOfTheDay(Activity activity) 
        {
            _Db.Activity.Attach(activity);

            Event eventToAdd = new Event()
            {
                ActivityName = activity,
                Attendees = new List<Attendance>(),
                Location = activity.PreferredLocation,
                Messages = new List<Message>(),
                Time = DateTime.Today
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
    }

    //protected class PreferredEventSorter : IComparer<Event>
    //{
    //    private AttendanceDb _attendanceDb = new AttendanceDb();
    //    private string username;

    //    public PreferredEventSorter(string username)
    //    {
    //        this.username = username;
    //    }

    //    int IComparer.Compare(object a, object b)
    //    {
    //        Event eventA = (Event)a;
    //        Event eventB = (Event)b;

    //        int eventAattendanceCount = this._attendanceDb.GetPlayerAttendanceForActivity(eventA.Activity.Name, username, DateTime.Today.AddMonths(-6));
    //        int eventBattendanceCount = this._attendanceDb.GetPlayerAttendanceForActivity(eventB.Activity.Name, username, DateTime.Today.AddMonths(-6));

    //        if (eventAattendanceCount > eventBattendanceCount) return 1;
    //        if (eventAattendanceCount < eventBattendanceCount) return -1;
    //        return 0;
    //    }
    //}
}