using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SportsHub.HelperMethods;
using SportsHub.Infrastructure;
using SportsHub.Models;

namespace SportsHub.Controllers
{
    public class AdminController : Controller
    {
        private EventDb _eventDb = new EventDb();
        private ActivityDb _activityDb = new ActivityDb();
        private PlayerDb _playerDb = new PlayerDb();
        private List<string> Admins = DataHelper.Admins(); 

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManageActivities()
        {
            var error = string.Empty;
            if (!Admins.Contains(User.Identity.Name))
            {
                error = "You do not have administrator priviledges!";
                return RedirectToAction("Index", "Event", new { message = error });
            }
            List<Activity> allActivities = _activityDb.GetAllActivities();
            return View(allActivities);
        }

        public ActionResult ManagePlayers()
        {
            var error = string.Empty;
            if (!Admins.Contains(User.Identity.Name))
            {
                error = "You do not have administrator priviledges!";
                return RedirectToAction("Index", "Event", new { message = error });
            }
            List<Player> allPlayers = _playerDb.GetAllPlayers();
            return View(allPlayers);
        }

        public ActionResult ManageLocations()
        {
            var error = string.Empty;
            if (!Admins.Contains(User.Identity.Name))
            {
                error = "You do not have administrator priviledges!";
                return RedirectToAction("Index", "Event", new { message = error });
            }
            var allActivities = _activityDb.GetAllActivities();
            var allLocations = from activity in allActivities
                               select activity.PreferredLocation;

            return View(allLocations);
        }

        public ActionResult ManageEventsOfTheDay()
        {
            var error = string.Empty;
            if (!Admins.Contains(User.Identity.Name))
            {
                error = "You do not have administrator priviledges!";
                return RedirectToAction("Index", "Event", new { message = error });
            }
            Player user = _playerDb.GetPlayerByUsername(User.Identity.Name);
            List<Activity> activitiesOfTheDay = _activityDb.GetActivitiesOfTheDay();
            List<Event> eventsOfTheDay = _eventDb.GetEventsOfTheDay(activitiesOfTheDay, user);

            return View(eventsOfTheDay);
        }

        public ActionResult SendEmail()
        {
            var error = string.Empty;
            if (!Admins.Contains(User.Identity.Name))
            {
                error = "You do not have administrator priviledges!";
                return RedirectToAction("Index", "Event", new { message = error });
            }
            //Player user = _playerDb.GetPlayerByUsername(User.Identity.Name);
            //List<Activity> activitiesOfTheDay = _activityDb.GetActivitiesOfTheDay();
            //List<Event> eventsOfTheDay = _eventDb.GetEventsOfTheDay(activitiesOfTheDay, user);

            //EMail message = new EMail();
            //List<string> listOfEvents = new List<string>();

            //foreach (var ev in eventsOfTheDay)
            //{
            //    listOfEvents.Add(ev.Activity.Name);
            //}

            //message.Subject = "An alert from UltiSports Hub!";
            //message.Body =
            //    "You have joined our mailing group, and today we have an activity! Go to: josealw7x6530/ to check the status of today's activities!";
            Player user = _playerDb.GetPlayerByUsername(User.Identity.Name);
            List<Activity> activitiesOfTheDay = _activityDb.GetActivitiesOfTheDay();
            List<Event> eventsOfTheDay = _eventDb.GetEventsOfTheDay(activitiesOfTheDay, user);
            List<string> ListOfEvents = new List<string>();
            foreach (var ev in eventsOfTheDay)
            {
                ListOfEvents.Add(ev.Activity.Name);
            }
            EMailServices.SendEmail(ListOfEvents);

            return RedirectToAction("ManageActivities", "Admin");
        }
    }
}
