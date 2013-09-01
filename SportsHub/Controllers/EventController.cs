using SportsHub.Infrastructure;
using SportsHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsHub.Controllers
{
    public class EventController : Controller
    {
        private EventDb controllerDb = new EventDb();
        private ActivityDb activityDb = new ActivityDb();

        public ActionResult Index(string message = null) 
        {
            List<Activity> activitiesOfTheDay = activityDb.GetActivitiesOfTheDay();
            List<Event> eventsOfTheDay = controllerDb.GetEventsOfTheDay(activitiesOfTheDay);

            return View(eventsOfTheDay);
        }
    }
}