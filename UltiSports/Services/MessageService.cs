using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UltiSports.Infrastructure;
using UltiSports.Models;

namespace UltiSports.Services
{
    public class MessageService : BaseService<Message>, IMessageService
    {
        private IEventRepository _eventDb;
        private IMessageRepository _messageDb;
        private IPlayerRepository _playerDb;

        public MessageService(IEventRepository eventDb, IMessageRepository messageDb, IPlayerRepository playerDb)
        : base(messageDb) {
            _eventDb = eventDb;
            _messageDb = messageDb;
            _playerDb = playerDb;
        }

        public ServiceData<Event> Create(string message, int eventId, string name)
        {
            string result = string.Empty;
            bool success = false;
            Event ev = _eventDb.GetByID(eventId);
            Player author = _playerDb.GetPlayerByUsername(name);
            Message newMessage = new Message
            {
                Author = author,
                Event = ev,
                MessageText = message,
                Time = DateTime.Now
            };

            try
            {
                _messageDb.Insert(newMessage);
                success = true;
            }
            catch (Exception ex)
            {
                result = "Message failed";
            }

            return new ServiceData<Event>(ev, result, success);
        }

        public ServiceData<IEnumerable<Message>> GetMessagesForEvent(int eventId)
        {
            // Add more logic to see if things succeeded.
            return new ServiceData<IEnumerable<Message>>(_messageDb.GetMessagesForEvent(eventId), "success", true);
        }
    }

    public interface IMessageService : IBaseService<Message>
    {
        ServiceData<Event> Create(string message, int eventId, string name);
        ServiceData<IEnumerable<Message>> GetMessagesForEvent(int eventId);
    }
}