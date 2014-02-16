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
        private ILocationService _locationService;

        public LocationController(ILocationService service)
        {
            _locationService = service;
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
                _locationService.Create(newLocation);
            }
            return RedirectToAction("ManageLocations", "Admin");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var response = _locationService.GetByID(Id);
            return View(response.Data);
        }

        [HttpPost]
        public ActionResult Edit(Location newLocation)
        {
            _locationService.Update(newLocation);
            return RedirectToAction("ManageLocations", "Admin");
        }

        [HttpGet]
        public ActionResult Delete(Location location)
        {
            _locationService.Delete(location);
            return RedirectToAction("ManageLocations", "Admin");
        }

        public ActionResult ToggleAvailable(int Id)
        {
            var locationService = _locationService.GetByID(Id);

            if (locationService.Data.IsActive)
                locationService.Data.IsActive = false;
            else
            {
                locationService.Data.IsActive = true;
            }

            _locationService.Update(locationService.Data);

            return RedirectToAction("ManageLocations", "Admin");
        }
    }
}
