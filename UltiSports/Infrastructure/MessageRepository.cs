using System;
using System.Collections.Generic;
using UltiSports.Models;

namespace UltiSports.Infrastructure
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(IGenericRepository<Message> repo) : base(repo)
        {

        }

        public IEnumerable<Message> GetMessagesForEvent(int eventId)
        {
            return _repo.Get(x => x.Event.Id == eventId);
        }
    }

    public interface IMessageRepository : IBaseRepository<Message>
    {
        IEnumerable<Message> GetMessagesForEvent(int eventId);
    }
}