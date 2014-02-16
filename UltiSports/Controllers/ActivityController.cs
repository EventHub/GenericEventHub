using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UltiSports.Filters;
using UltiSports.Infrastructure;
using UltiSports.Models;
using UltiSports.Services;

namespace UltiSports.Controllers
{
    [AdminAuthentication]
    public class ActivityController : Controller
    {
        private IActivityService _activityService;
        private ILocationService _locationService;

        public ActivityController(IActivityService service,
            ILocationService locationService)
        {
            _activityService = service;
            _locationService = locationService;
        }

        [HttpGet]
        public ActionResult Create()
        {
            var response = _locationService.GetAllActiveLocations();
            ViewBag.AllLocations = response;

            return View();
        }

        [HttpPost]
        public ActionResult Create(Activity newActivity)
        {
            // Do validation in service
            if (ModelState.IsValid)
            {
                var response = _activityService.Create(newActivity);
                ViewBag.Message = response.Message;
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
            ServiceData<Activity> response = _activityService.GetByID(id);
            ViewBag.AllLocations = _locationService.GetAllActiveLocations();
            return View(response.Data);
        }

        //TODO: Change this to use a ViewModel specifically to retrieve the values that need to be changed.
        [HttpPost]
        public ActionResult Edit(Activity editedActivity)
        {
            editedActivity.PreferredLocation = _locationService.GetByID(editedActivity.PreferredLocation.Id).Data;
            _activityService.UpdateActivity(editedActivity);
            return RedirectToAction("ManageActivities", "Admin");
        }

        public ActionResult ToggleActive(string id)
        {
            var activityToCancel = _activityService.GetByID(id);

            if (activityToCancel.Data.IsActive)
                activityToCancel.Data.IsActive = false;
            else
            {
                activityToCancel.Data.IsActive = true;
            }

            _activityService.Update(activityToCancel.Data);

            return RedirectToAction("ManageActivities", "Admin");
        }
    }
}