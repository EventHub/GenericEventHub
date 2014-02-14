using System.Web.Mvc;
using UltiSports.Filters;
using UltiSports.Infrastructure;
using UltiSports.Models;
using UltiSports.Services;

namespace UltiSports.Controllers
{
    [AdminAuthentication]
    public class LocationController : Controller
    {
        private ILocationService _service;

        public LocationController(ILocationService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Location newLocation)
        {

            if (ModelState.IsValid)
            {
                _service.Create(newLocation);
            }
            return RedirectToAction("ManageLocations", "Admin");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var response = _service.GetByID(Id);
            return View(response.Data);
        }

        [HttpPost]
        public ActionResult Edit(Location newLocation)
        {
            _service.Update(newLocation);
            return RedirectToAction("ManageLocations", "Admin");
        }

        [HttpGet]
        public ActionResult Delete(Location location)
        {
            _service.Delete(location);
            return RedirectToAction("ManageLocations", "Admin");
        }
    }
}
