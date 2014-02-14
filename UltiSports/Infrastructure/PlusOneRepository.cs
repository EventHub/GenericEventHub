using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UltiSports.Models;

namespace UltiSports.Infrastructure
{
    public class PlusOneRepository : BaseRepository<PlusOne>, UltiSports.Infrastructure.IPlusOneRepository
    {
        public PlusOneRepository(IGenericRepository<PlusOne> repo)
            : base(repo)
        {

        }

        public bool IsAttending(PlusOne plusOne, Event ev)
        {
            bool result = false;
            var foundPlusOne = _repo.Get(x => x.Event.Id.Equals(ev.Id) && x.Name.Equals(plusOne.Name));
            if (foundPlusOne != null)
                result = true;

            return result;
        }
    }

    public interface IPlusOneRepository : IBaseRepository<PlusOne>
    {
        bool IsAttending(PlusOne plusOne, Event ev);
    }
}