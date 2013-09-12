using SportsHub.Infrastructure;
using SportsHub.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SportsHub.Controllers
{
    public class EventController : Controller
    {
        private EventDb _controllerDb = new EventDb();
        private ActivityDb _activityDb = new ActivityDb();
        private PlayerDb _playerDb = new PlayerDb();

        //TODO: Autenticacion sera para 
        public ActionResult Index(string message = null)
        {
            bool isRegistered = _playerDb.isARegisteredPlayer(@User.Identity.Name);
            if (!isRegistered)
            {
                return RedirectToAction("Register", "Player");
            }

            List<Activity> activitiesOfTheDay = _activityDb.GetActivitiesOfTheDay();
            List<Event> eventsOfTheDay = _controllerDb.GetEventsOfTheDay(activitiesOfTheDay);
            ViewBag.Message = message;

            return View(eventsOfTheDay);
        }
    }
}