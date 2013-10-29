using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SportsHub.Infrastructure;
using SportsHub.Models;

namespace SportsHub.Controllers
{
    public class ActivityController : Controller
    {
        private ActivityDb _activityDb = new ActivityDb();
        private EventDb _eventDb = new EventDb();

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Activity newActivity)
        {
            var allActivities = _activityDb.GetAllActivities();
            IEnumerable<Location> allLocations = from activity in allActivities
                                                 select activity.PreferredLocation;

            foreach (var location in allLocations)
            {
                if (location.Name == newActivity.PreferredLocation.Name)
                {
                    newActivity.PreferredLocation = location;
                    break;
                }
            }

            if (ModelState.IsValid)
            {
                _activityDb.AddEntity(newActivity);
            }
            else
            {
                ViewBag.Message = "Activity could not be added due to invalid fields!";
            }

            return RedirectToAction("ManageActivities", "Admin");
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            Activity activityToEdit = _activityDb.GetActivityByName(id);
            return View(activityToEdit);
        }

        //TODO: Change this to use a ViewModel specifically to retrieve the values that need to be changed.
        [HttpPost]
        public ActionResult Edit(Activity editedActivity)
        {
            var allActivities = _activityDb.GetAllActivities();
            IEnumerable<Location> allLocations = from activity in allActivities
                                               select activity.PreferredLocation;

            foreach (var location in allLocations)
            {
                if (location.Name == editedActivity.PreferredLocation.Name)
                {
                    editedActivity.PreferredLocation = location;
                    break;
                }
            }

            if (true/*ModelState.IsValid*/)
            {
                var activityToUpdate = _activityDb.GetActivityByName(editedActivity.Name);
                activityToUpdate.PreferredTime = editedActivity.PreferredTime;
                activityToUpdate.PreferredLocation = editedActivity.PreferredLocation;

                activityToUpdate.DayOfTheWeek = editedActivity.DayOfTheWeek;

                activityToUpdate.RequiredNumberOfPlayers = editedActivity.RequiredNumberOfPlayers;
                activityToUpdate.RecommendedNumberOfPlayers = editedActivity.RecommendedNumberOfPlayers;
                _activityDb.UpdateEntity(activityToUpdate);
            }

            return RedirectToAction("ManageActivities", "Admin");
        }

        public ActionResult DeleteActivity(string id)
        {
            var activityToDelete = _activityDb.GetActivityByName(id);
            _activityDb.DeleteEntity(activityToDelete);
            //if (_eventDb.IsInAnExistingEvent(activityToDelete))
            //{
            //    _activityDb.DeleteEntity(activityToDelete);
            //}
            //else
            //{
            //    ViewBag.Message = "Warning: this activity is part of an existing event. Please delete event first.";
            //}

            return RedirectToAction("ManageActivities", "Admin");
        }
    }
}
