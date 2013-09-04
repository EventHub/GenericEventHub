using System.Web.Mvc;
using SportsHub.Infrastructure;
using SportsHub.Models;

namespace SportsHub.Controllers
{
    public class MessageController : Controller
    {
        private PlayerDb _playerDb = new PlayerDb();
        private EventDb _eventDb = new EventDb();
        private MessageDb _messageDb = new MessageDb();

        public ActionResult PostMessage(Message newMessage)
        {
            string error = string.Empty;

            if (ModelState.IsValid)
            {
                var author = _playerDb.GetPlayerByUsername(User.Identity.Name);
                newMessage.Author = author;

                newMessage.Event = _eventDb.GetEventById(newMessage.Event.Id);
                error = _messageDb.AddMessage(newMessage);
            }

            return RedirectToAction("Index", "Event", new {errorMessage = error});
        }

    }
}
