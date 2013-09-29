using System.Web.Mvc;
using SportsHub.Infrastructure;
using SportsHub.Models;

namespace SportsHub.Controllers
{
    public class ActivityController : Controller
    {
        private ActivityDb _db = new ActivityDb();

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Activity newActivity)
        {
            if (ModelState.IsValid)
            {
                _db.AddEntity(newActivity);
            }
            return RedirectToAction("ManageActivities", "Admin");
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            Activity activityToEdit = _db.GetActivityByName(id);
            return View(activityToEdit);
        }

        [HttpPost]
        public ActionResult Edit(Activity editedActivity)
        {
            if (ModelState.IsValid)
            {
                var activityToUpdate = _db.GetActivityByName(editedActivity.Name);
                activityToUpdate.PreferredTime = editedActivity.PreferredTime;
                activityToUpdate.PreferredLocation = editedActivity.PreferredLocation;

                activityToUpdate.RequiredNumberOfPlayers = editedActivity.RequiredNumberOfPlayers;
                activityToUpdate.RecommendedNumberOfPlayers = editedActivity.RecommendedNumberOfPlayers;
                _db.UpdateEntity(activityToUpdate);
            }

            return RedirectToAction("ManageActivities", "Admin");
        }

        public ActionResult Delete(string id)
        {
            var activityToDelete = _db.GetActivityByName(id);
            _db.DeleteEntity(activityToDelete);

            return RedirectToAction("ManageActivities", "Admin");
        }
    }
}
