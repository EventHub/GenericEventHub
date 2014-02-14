using System.Web.Mvc;
using UltiSports.Infrastructure;
using UltiSports.Services;

namespace UltiSports.Controllers
{
    public class EventController : Controller
    {
        private IEventService _service;

        public EventController(IEventService service)
        {
            _service = service;
        }

        public ActionResult Delete()
        {
            return RedirectToAction("ManageEventsOfTheDay", "Admin");
        }
    }
}