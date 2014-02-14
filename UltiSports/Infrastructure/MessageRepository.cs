using System;
using UltiSports.Models;

namespace UltiSports.Infrastructure
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(IGenericRepository<Message> repo) : base(repo)
        {

        }
    }

    public interface IMessageRepository : IBaseRepository<Message>
    {

    }
}