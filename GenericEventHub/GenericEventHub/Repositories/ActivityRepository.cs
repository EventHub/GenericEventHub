using GenericEventHub.Models;
using System.Collections.Generic;

namespace GenericEventHub.Repositories
{
    public class ActivityRepository : BaseRepository<Activity>, IActivityRepository
    {
        public ActivityRepository(IGenericRepository<Activity> repo) : base(repo)
        {
        }

        public IEnumerable<Activity> GetActivitiesOn(string dayOfWeek)
        {
            return _repo.Get(x => x.DayOfWeek.Equals(dayOfWeek));
        }


    }

    public interface IActivityRepository : IBaseRepository<Activity>
    {
        IEnumerable<Activity> GetActivitiesOn(string dayOfWeek);
    }
}