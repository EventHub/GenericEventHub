using SportsHub.Infrastructure;
using System.Web.Mvc;
using SportsHub.Models;

namespace SportsHub.Controllers
{
    public class AttendanceController : Controller
    {
        private AttendanceDb _attendanceDb = new AttendanceDb();
        private EventDb _eventDb = new EventDb();
        private PlayerDb _playerDb = new PlayerDb();

        public ActionResult AttendEvent(int id)
        {
            var eventToAttend = _eventDb.GetEventById(id);
            var player = _playerDb.GetPlayerByUsername(User.Identity.Name);
            string errorMessage = _attendanceDb.AttendEvent(eventToAttend, player);

            return RedirectToAction("Index", "Event", new { message = errorMessage });
        }

        public ActionResult AddPlusOne(int id)
        {
            var eventToAttend = _eventDb.GetEventById(id);
            var player = _playerDb.GetPlayerByUsername(User.Identity.Name);
            var errorMessage = _attendanceDb.AddPlusOne(eventToAttend, player);

            return RedirectToAction("Index", "Event", new { message = errorMessage });
        }

        public ActionResult LeaveEvent(int id)
        {
            var eventToAttend = _eventDb.GetEventById(id);
            var player = _playerDb.GetPlayerByUsername(User.Identity.Name);
            string errorMessage = _attendanceDb.LeaveEvent(eventToAttend, player);

            return RedirectToAction("Index", "Event", new { message = errorMessage });
        }
    }
}