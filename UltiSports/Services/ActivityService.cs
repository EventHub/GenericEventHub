using UltiSports.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UltiSports.Models;
using System;

namespace UltiSports.Services
{
    public class ActivityService : BaseService<Activity>, UltiSports.Services.IActivityService
    {
        private IActivityRepository _activityDb;
        private ILocationRepository _locationDb;

        public ActivityService(IActivityRepository activityDb,
            ILocationRepository locationDb) : base(activityDb) {
                _activityDb = activityDb;
                _locationDb = locationDb;
        }

        public ServiceResponse Create(Activity newActivity)
        {
            string message = string.Empty;
            bool success = false;

            try
            {
                newActivity.PreferredLocation = _locationDb.GetLocationById(newActivity.PreferredLocation.Id);
                _activityDb.Insert(newActivity);
                message = "Great success!";
                success = true;
            }
            catch (Exception ex)
            {
                message = "Fail!";
            }

            return new ServiceResponse(message, success);
        }

        public ServiceData<Activity> GetByName(string name) {
            string message = string.Empty;
            bool success = false;
            Activity activity = null;

            try {
                activity = _activityDb.GetActivityByName(name);
                message = "Get successfull!";
                success = true;
            }
            catch(Exception ex) {
                message = "Fail";
            }
            return new ServiceData<Activity>(activity, message, success);
        }

        public override ServiceResponse Update(Activity editedActivity) {
            string message = string.Empty;
            bool success = false;

            try
            {
                editedActivity.PreferredLocation = _locationDb.GetLocationById(editedActivity.PreferredLocation.Id);
                _activityDb.Update(editedActivity);
                message = "Great success!";
                success = true;
            }
            catch (Exception ex)
            {
                message = "Fail!";
            }

            return new ServiceResponse(message, success);
        }

        public IEnumerable<Activity> GetActivitiesFor(string dayOfWeek)
        {
            return _activityDb.GetActivitiesFor(dayOfWeek);
        }

        public IEnumerable<Activity> GetActiveActivitiesFor(string dayOfWeek)
        {
            return _activityDb.GetActiveActivitiesFor(dayOfWeek);
        }

        public ServiceResponse UpdateActivity(Activity editedActivity)
        {
            string message = string.Empty;
            bool success = false;

            try
            {
                _activityDb.UpdateActivity(editedActivity);
                message = "Great success!";
                success = true;
            }
            catch (Exception ex)
            {
                message = "Fail!";
            }

            return new ServiceResponse(message, success);
        }
    }

    public interface IActivityService : IBaseService<Activity>
    {
        ServiceResponse Create(UltiSports.Models.Activity newActivity);
        System.Collections.Generic.IEnumerable<UltiSports.Models.Activity> GetActivitiesFor(string dayOfWeek);
        ServiceData<UltiSports.Models.Activity> GetByName(string name);

        ServiceResponse Update(UltiSports.Models.Activity editedActivity);
        IEnumerable<Activity> GetActiveActivitiesFor(string dayOfWeek);
        ServiceResponse UpdateActivity(UltiSports.Models.Activity editedActivity);
    }
}