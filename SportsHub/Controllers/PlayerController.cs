using SportsHub.Infrastructure;
using System;
using System.Web.Mvc;
using Player = SportsHub.Models.Player;

namespace SportsHub.Controllers
{
    public class PlayerController : Controller
    {
        private PlayerDb _playerDb = new PlayerDb();

        [HttpPost]
        public ActionResult Register(Player newPlayer)
        {
            string resultMessage = String.Empty;
            if (ModelState.IsValid)
            {
                newPlayer.Username = User.Identity.Name;
                resultMessage = _playerDb.RegisterPlayer(newPlayer);
            }

            return RedirectToAction("Index", "Event", new { message = resultMessage });
        }

        [HttpPost]
        public ActionResult Remove() 
        {
            string resultMessage = string.Empty;
            
            if (ModelState.IsValid) 
            {
                resultMessage = _playerDb.InactivatePlayer(User.Identity.Name);
            }

            return RedirectToAction("Index", "Event", new { message = resultMessage });
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
    }
}
