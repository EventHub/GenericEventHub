using System;
using System.Web.Mvc;
using UltiSports.Filters;
using UltiSports.Infrastructure;
using UltiSports.Models;
using UltiSports.Services;

namespace UltiSports.Controllers
{
    public class PlayerController : Controller
    {
        private IPlayerService _service;

        public PlayerController(IPlayerService service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult Register(Player newPlayer)
        {
            string resultMessage = String.Empty;
            if (true/*ModelState.IsValid*/)
            {
                newPlayer.Username = User.Identity.Name;
                resultMessage = _service.Register(newPlayer);
            }

            return RedirectToAction("Index", "Home", new { message = resultMessage });
        }

        #region Edit
        [HttpGet]
        public ActionResult Edit(string username)
        {
            var model = _service.GetByID(username);
            return View(model.Data);
        }

        [HttpPost]
        public ActionResult Edit(Player editedPlayer)
        {
            _service.Update(editedPlayer);
            return RedirectToAction("ManagePlayers", "Admin", new { message = "Player edited." });
        }
        #endregion

        [HttpPost]
        [AdminAuthentication]
        public ActionResult Remove()
        {
            string resultMessage = string.Empty;

            if (ModelState.IsValid)
            {
                resultMessage = _service.InactivatePlayer(User.Identity.Name);
            }

            return RedirectToAction("Index", "Home", new { message = resultMessage });
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
    }
}