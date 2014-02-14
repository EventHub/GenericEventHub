using System;
using System.Collections.Generic;
using System.Web.Mvc;
using UltiSports.Filters;
using UltiSports.HelperMethods;
using UltiSports.Infrastructure;
using UltiSports.Models;
using UltiSports.Services;

namespace UltiSports.Controllers
{
    [AdminAuthentication]
    public class AdminController : Controller
    {
        private IAdminService _service;
        private IEmailService _emailService;

        public AdminController(IAdminService service,
            IEmailService emailService)
        {
            _service = service;
            _emailService = emailService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManageActivities()
        {
            var error = string.Empty;

            var allActivities = _service.GetAllActivites();
            return View(allActivities);
        }

        public ActionResult ManagePlayers()
        {
            var error = string.Empty;
            
            var allPlayers = _service.GetAllPlayers();
            return View(allPlayers);
        }

        public ActionResult ManageLocations()
        {
            var error = string.Empty;
            var allLocations = _service.GetAllLocations();

            return View(allLocations);
        }

        public ActionResult ManageEventsOfTheDay()
        {
            var error = string.Empty;
            var dayOfWeek = DateTime.Now.Date.DayOfWeek.ToString();

            var eventsOfTheDay = _service.GetEventsFor(dayOfWeek, User.Identity.Name);

            return View(eventsOfTheDay);
        }

        public ActionResult SendFinalEmail()
        {
            var error = string.Empty;
            var dayOfWeek = DateTime.Now.Date.DayOfWeek.ToString();
            _emailService.SendFinalEmail(dayOfWeek);

            return RedirectToAction("ManageActivities", "Admin");
        }

        public ActionResult SendEmail()
        {
            var error = string.Empty;
            var dayOfWeek = DateTime.Now.Date.DayOfWeek.ToString();

            _emailService.SendFirstEmail(dayOfWeek);

            return RedirectToAction("ManageActivities", "Admin");
        }
    }
}