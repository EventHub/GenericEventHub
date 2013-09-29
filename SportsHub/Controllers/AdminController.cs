using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SportsHub.Infrastructure;
using SportsHub.Models;

namespace SportsHub.Controllers
{
    public class AdminController : Controller
    {
        private ActivityDb _activityDb = new ActivityDb();
        private PlayerDb _playerDb = new PlayerDb();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManageActivities()
        {
            List<Activity> allActivities = _activityDb.GetAllActivities();
            return View(allActivities);
        }

        public ActionResult ManagePlayers()
        {
            List<Player> allPlayers = _playerDb.GetAllPlayers();
            return View(allPlayers);
        }

        public ActionResult ManageLocations()
        {
            var allActivities = _activityDb.GetAllActivities();
            var allLocations = from activity in allActivities
                               select activity.PreferredLocation;

            return View(allLocations);
        }
    }
}
