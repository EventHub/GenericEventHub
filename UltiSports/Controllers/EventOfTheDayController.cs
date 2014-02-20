using System;
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
        private IEventService _eventService;
        private ILocationService _locationService;
        private IActivityService _activityService;

        private IPlayerService _playerService;
        private IAttendanceService _attendanceService;
        private IEmailService _emailService;

        public EventOfTheDayController(IEventService service,
            ILocationService locationService,
            IActivityService activityService,
            IPlayerService playerService,
            IAttendanceService attendanceService,
            IEmailService emailService)
        {
            _eventService = service;
            _locationService = locationService;
            _activityService = activityService;

            _playerService = playerService;
            _attendanceService = attendanceService;
            _emailService = emailService;
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var response = _eventService.GetByID(id);
            var model = response.Data;
            ViewBag.AllActivities = _activityService.GetAll().Data;
            ViewBag.AllLocations = _locationService.GetAll().Data;

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Event eventToUpdate)
        {
            var result = RidiculousHelperBecauseIDontHaveResetContextYet(eventToUpdate);
            _eventService.Update(result);

            return RedirectToAction("ManageEventsOfTheDay", "Admin");
        }

        public ActionResult Cancel(int Id)
        {
            var eventToCancel = _eventService.GetByID(Id);

            if (eventToCancel.Data.IsCanceled)
                eventToCancel.Data.IsCanceled = false;
            else
            {
                eventToCancel.Data.IsCanceled = true;
                _emailService.SendCancellationEmail(DateTime.Now.Date.DayOfWeek.ToString());
            }

            _eventService.Update(eventToCancel.Data);

            return RedirectToAction("ManageEventsOfTheDay", "Admin");
        }

        private Event RidiculousHelperBecauseIDontHaveResetContextYet(Event eventToUpdate)
        {
            var result = _eventService.GetByID(eventToUpdate.Id);
            result.Data.Id = eventToUpdate.Id;
            result.Data.Messages = eventToUpdate.Messages;

            result.Data.PlusOnes = eventToUpdate.PlusOnes;
            result.Data.Time = eventToUpdate.Time;
            result.Data.Attendees = eventToUpdate.Attendees;

            result.Data.IsCanceled = eventToUpdate.IsCanceled;
            // Grab the existing entity for the activity selected and associated it to event
            result.Data.Activity = _activityService.GetByID(eventToUpdate.Activity.Name).Data;
            result.Data.Location = _locationService.GetByID(eventToUpdate.Location.Id).Data; // Possible issue with duplicate location names. Might need to add an attribute to prevent this.

            return result.Data;
        }
    }
}
