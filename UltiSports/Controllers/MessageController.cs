using System;
using System.Web.Mvc;
using UltiSports.Infrastructure;
using UltiSports.Models;
using UltiSports.Services;

namespace UltiSports.Controllers
{
    public class MessageController : Controller
    {
        private IMessageService _service;

        public MessageController(IMessageService service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult Create(string message, int eventId)
        {
            string error = string.Empty;

            var response = _service.Create(message, eventId, User.Identity.Name);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Comments", response.Data);
            }

            return RedirectToAction("Index", "Event", new { errorMessage = error });
        }
    }
}