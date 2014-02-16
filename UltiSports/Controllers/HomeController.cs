using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UltiSports.Services;

namespace UltiSports.Controllers
{
    public class HomeController : Controller
    {
        private IPlayerService _playerService;
        private IActivityService _activityService;
        private IEventService _eventService;

        public HomeController (IPlayerService playerService,
            IActivityService activityService,
            IEventService eventService)
        {
            _playerService = playerService;
            _activityService = activityService;
            _eventService = eventService;
        }

        public ActionResult Index(string message = null)
        {
            string username = @User.Identity.Name;
            bool isRegistered = _playerService.IsRegistered(username);
            if (!isRegistered)
            {
                return RedirectToAction("Register", "Player");
            }

            var response = _playerService.GetByID(username);
            var user = response.Data;
            var dayOfWeek = DateTime.Now.Date.DayOfWeek.ToString();
            var activitiesOfTheDay = _activityService.GetActiveActivitiesFor(dayOfWeek);

            var eventsOfTheDay = _eventService.GetEventsOfTheDay(activitiesOfTheDay, user);
            ViewBag.Message = message;

            return View(eventsOfTheDay);
        }

    }
}