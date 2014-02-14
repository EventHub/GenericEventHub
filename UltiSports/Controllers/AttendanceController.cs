using System.Web.Mvc;
using UltiSports.Infrastructure;
using UltiSports.Models;
using UltiSports.Services;

namespace UltiSports.Controllers
{
    public class AttendanceController : Controller
    {
        private IAttendanceService _service;

        public AttendanceController(IAttendanceService service)
        {
            _service = service;
        }

        public ActionResult AddPlayer(int id)
        {
            string errorMessage = _service.AddPlayer(id, User.Identity.Name);

            return RedirectToAction("Index", "Home", new { message = errorMessage });
        }

        public ActionResult AddPlusOne(int eventId, string plusOneName)
        {
            var resultMessage = _service.AddPlusOne(eventId, User.Identity.Name, plusOneName);

            return RedirectToAction("Index", "Home", new { message = resultMessage });
        }

        public ActionResult RemovePlusOne(int id, int plusOneId)
        {
            string errorMessage = _service.RemovePlusOne(id, plusOneId);

            return RedirectToAction("Index", "Home", new { message = errorMessage });
        }

        public ActionResult RemovePlayer(int id)
        {
            string errorMessage = _service.RemovePlayer(id, User.Identity.Name);

            return RedirectToAction("Index", "Home", new { message = errorMessage });
        }
    }
}