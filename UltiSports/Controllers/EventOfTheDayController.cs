using System.Web.Mvc;
using UltiSports.Filters;
using UltiSports.Infrastructure;
using UltiSports.Models;
using UltiSports.Services;

namespace UltiSports.Controllers
{
    [AdminAuthentication]
    public class EventOfTheDayController : Controller
    {
        private IEventService _service;
        private ILocationService _locationService;
        private IActivityService _activityService;

        public EventOfTheDayController(IEventService service,
            ILocationService locationService,
            IActivityService activityService)
        {
            _service = service;
            _locationService = locationService;
            _activityService = activityService;
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var response = _service.GetByID(id);
            var model = response.Data;
            ViewBag.AllActivities = _activityService.GetAll().Data;
            ViewBag.AllLocations = _locationService.GetAll().Data;

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Event eventToUpdate)
        {
            // Grab the existing entity for the activity selected and associated it to event
            eventToUpdate.Activity = _activityService.GetByID(eventToUpdate.Activity.Name).Data;
            eventToUpdate.Location = _locationService.GetByID(eventToUpdate.Location.Id).Data; // Possible issue with duplicate location names. Might need to add an attribute to prevent this.
            _service.Update(eventToUpdate);

            return RedirectToAction("ManageEventsOfTheDay", "Admin");
        }
    }
}
