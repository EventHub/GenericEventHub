using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UltiSports.Models;

namespace UltiSports.Infrastructure
{
    public class ActivityRepository : BaseRepository<Activity>, IActivityRepository
    {
        public ActivityRepository(IGenericRepository<Activity> repo) : base(repo)
        {
        }

        public IEnumerable<Activity> GetActivitiesFor(string dayOfWeek)
        {
            return _repo.Get(x => x.DayOfTheWeek.Equals(dayOfWeek));
        }

        public IEnumerable<Activity> GetActive()
        {
            return _repo.Get(x => x.IsActive);
        }

        public Activity GetActivityByName(string name)
        {
            return _repo.GetByID(name);
        }

        public IEnumerable<Activity> GetActiveActivitiesFor(string dayOfWeek)
        {
            return _repo.Get(act => act.DayOfTheWeek.Equals(dayOfWeek) && act.IsActive);
        }

        public void CreateActivity(Activity activityToCreate)
        {
            _repo.CreateActivity(activityToCreate);
        }

        public void UpdateActivity(Activity editedLocation)
        {
            _repo.UpdateActivity(editedLocation);
        }
    }

    public interface IActivityRepository : IBaseRepository<Activity>
    {
        IEnumerable<Activity> GetActivitiesFor(string dayOfWeek);
        Activity GetActivityByName(string name);
        IEnumerable<UltiSports.Models.Activity> GetActive();
        IEnumerable<Activity> GetActiveActivitiesFor(string dayOfWeek);
        void UpdateActivity(Activity editedLocation);
        void CreateActivity(Activity editedLocation);
    }
}