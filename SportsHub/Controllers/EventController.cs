using System;
using SportsHub.Infrastructure;
using SportsHub.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using SportsHub.Filter;

namespace SportsHub.Controllers
{
    public class EventController : Controller
    {
        private EventDb _controllerDb = new EventDb();
        private ActivityDb _activityDb = new ActivityDb();
        private PlayerDb _playerDb = new PlayerDb();

        [CreatePlayerFilter]
        public ActionResult Index(string message = null)
        {
            string username = @User.Identity.Name;
            bool isRegistered = _playerDb.isARegisteredPlayer(username);
            if (!isRegistered)
            {
                return RedirectToAction("Register", "Player");
            }
            else
            {
                Player user = _playerDb.GetPlayerByUsername(username);
                List<Activity> activitiesOfTheDay = _activityDb.GetActivitiesOfTheDay();
                List<Event> eventsOfTheDay = _controllerDb.GetEventsOfTheDay(activitiesOfTheDay, user);
                ViewBag.Message = message;

                return View(eventsOfTheDay);
            }
        }
    }
}