using SportsHub.Infrastructure;
using System.Web.Mvc;

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
    }
}