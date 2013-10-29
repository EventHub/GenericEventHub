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

        public ActionResult AddPlusOne(int eventId, string plusOneName)
        {
            var eventToAttend = _eventDb.GetEventById(eventId);
            var player = _playerDb.GetPlayerByUsername(User.Identity.Name);
            var resultMessage = _attendanceDb.AddPlusOne(eventToAttend, player, plusOneName);
        
            return RedirectToAction("Index", "Event", new { message = resultMessage });
        }

        public ActionResult RemovePlusOne(int id, int plusOneId)
        {
            Event currentEvent = _eventDb.GetEventById(id);
            PlusOne plusOneToRemove = currentEvent.PlusOnes.Find(p => p.Id == plusOneId);
            string errorMessage = _attendanceDb.RemovePlusOne(currentEvent, plusOneToRemove);

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