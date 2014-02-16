﻿using System;
using System.Collections.Generic;
using System.Linq;
using UltiSports.Models;

namespace UltiSports.Infrastructure
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(IGenericRepository<Event> repo) : base(repo)
        {

        }

        public Event CreateEventOfTheDay(Activity activity)
        {
            Event eventToAdd = new Event()
            {
                Activity = activity,
                Location = activity.PreferredLocation,
                Messages = new List<Message>(),
                Attendees = new List<Player>(),
                PlusOnes = new List<PlusOne>(),
                Time = Convert.ToDateTime(activity.PreferredTime.ToString()),
            };

            Insert(eventToAdd);
            return eventToAdd;
        }

        internal bool IsInAnExistingEvent(Activity activity)
        {
            return _repo.Get(x => x.Activity.Name.Equals(activity.Name)).Count() > 0;
        }

        public IEnumerable<Event> GetEventsFor(string dayOfWeek)
        {
            return _repo.Get(x => x.Activity.DayOfTheWeek.Equals(dayOfWeek));
		}

        public IEnumerable<Event> GetEventsFor(DateTime date)
        {
            return _repo.Get(x => x.Time.Day == date.Day
                && x.Time.Month == date.Month 
                && x.Time.Year == date.Year);
        }

        public IEnumerable<Event> GetActiveEventsFor(string dayOfWeek)
        {
            return _repo.Get(x => x.Activity.DayOfTheWeek.Equals(dayOfWeek)
                && x.IsCanceled == false);
        }

        public IEnumerable<Event> GetActiveEventsFor(DateTime date)
        {
            return GetEventsFor(date).Where(x => x.IsCanceled == false);
        }

        public IEnumerable<Event> GetSubsetOfEventsFor(IEnumerable<Activity> activities, string dayOfWeek)
        {
            var events = from ev in GetEventsFor(dayOfWeek)
                         join a in activities
                            on ev.Activity.Name equals a.Name
                         select ev;
            return events;
        }
    }

    public interface IEventRepository : IBaseRepository<Event>
    {
        IEnumerable<Event> GetEventsFor(string dayOfWeek);
        IEnumerable<Event> GetActiveEventsFor(string dayOfWeek);
        IEnumerable<Event> GetActiveEventsFor(DateTime date);
        Event CreateEventOfTheDay(Activity activity);
        IEnumerable<Event> GetSubsetOfEventsFor(IEnumerable<Activity> activities, string dayOfWeek);
        IEnumerable<Event> GetEventsFor(DateTime date);
    }
}