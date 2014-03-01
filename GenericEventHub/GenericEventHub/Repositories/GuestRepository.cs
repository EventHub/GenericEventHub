using GenericEventHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericEventHub.Repositories
{
    public class GuestRepository : BaseRepository<Guest>, IGuestRepository
    {
        public GuestRepository(IGenericRepository<Guest> repo)
            : base(repo)
        {

        }

        public bool IsAttending(Guest Guest, Event ev)
        {
            bool result = false;
            var foundGuest = _repo.Get(x => x.Event.EventID.Equals(ev.EventID) && x.Name.Equals(Guest.Name));
            if (foundGuest != null)
                result = true;

            return result;
        }
    }

    public interface IGuestRepository : IBaseRepository<Guest>
    {
        bool IsAttending(Guest Guest, Event ev);
    }
}