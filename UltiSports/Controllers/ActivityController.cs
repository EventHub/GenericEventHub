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
        private IActivityService _service;
        private ILocationService _locationService;

        public ActivityController(IActivityService service,
            ILocationService locationService)
        {
            _service = service;
            _locationService = locationService;
        }

        [HttpGet]
        public ActionResult Create()
        {
            var response = _locationService.GetAll();
            ViewBag.AllLocations = response.Data;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Activity newActivity)
        {
            // Do validation in service
            if (ModelState.IsValid)
            {
                var response = _service.Create(newActivity);
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
            ServiceData<Activity> response = _service.GetByID(id);
            ViewBag.AllLocations = _locationService.GetAll().Data;
            return View(response.Data);
        }

        //TODO: Change this to use a ViewModel specifically to retrieve the values that need to be changed.
        [HttpPost]
        public ActionResult Edit(Activity editedActivity)
        {
            editedActivity.PreferredLocation = _locationService.GetByID(editedActivity.PreferredLocation.Id).Data;
            _service.Update(editedActivity);
            return RedirectToAction("ManageActivities", "Admin");
        }

        public ActionResult DeleteActivity(string id)
        {
            _service.Delete(id);
            //if (_eventDb.IsInAnExistingEvent(activityToDelete))
            //{
            //    ActivityRepository.DeleteEntity(activityToDelete);
            //}
            //else
            //{
            //    ViewBag.Message = "Warning: this activity is part of an existing event. Please delete event first.";
            //}

            return RedirectToAction("ManageActivities", "Admin");
        }
    }
}