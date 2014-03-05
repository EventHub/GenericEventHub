using GenericEventHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericEventHub.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(IGenericRepository<Event> repo) : base(repo)
        {

        }

        internal bool IsInAnExistingEvent(Activity activity)
        {
            return _repo.Get(x => x.Activity.Name.Equals(activity.Name)).Count() > 0;
        }

        public IEnumerable<Event> GetEventsOn(string dayOfWeek)
        {
            return _repo.Get(x => x.Activity.DayOfWeek.Equals(dayOfWeek));
		}

        public IEnumerable<Event> GetEventsOn(DateTime date)
        {
            return _repo.Get(x => x.DateTime.Day == date.Day
                && x.DateTime.Month == date.Month
                && x.DateTime.Year == date.Year);
        }

        public IEnumerable<Event> GetEventsAfter(DateTime date)
        {
            return _repo.Get(x => x.DateTime.Day >= date.Day
                && x.DateTime.Month >= date.Month
                && x.DateTime.Year >= date.Year);
        }
    }

    public interface IEventRepository : IBaseRepository<Event>
    {
        IEnumerable<Event> GetEventsOn(string dayOfWeek);
        IEnumerable<Event> GetEventsOn(DateTime date);
        IEnumerable<Event> GetEventsAfter(DateTime date);
    }
}