using SportsHub.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Player = SportsHub.Models.Player;

namespace SportsHub.Controllers
{
    public class PlayerController : Controller
    {
        private PlayerDb _db = new PlayerDb();

        [HttpPost]
        public ActionResult Add(Player player) 
        {
            string resultMessage = string.Empty;

            if (ModelState.IsValid) 
            {
                player.Username = User.Identity.Name;
                resultMessage = _db.AddPlayer(player);
            }

            return RedirectToAction("Index", "Event", new { message = resultMessage });
        }

        [HttpPost]
        public ActionResult Remove() 
        {
            string resultMessage = string.Empty;
            
            if (ModelState.IsValid) 
            {
                resultMessage = _db.InactivatePlayer(User.Identity.Name);
            }

            return RedirectToAction("Index", "Event", new { message = resultMessage });
        }
    }
}
