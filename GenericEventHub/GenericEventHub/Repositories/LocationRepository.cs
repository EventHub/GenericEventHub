using GenericEventHub.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace GenericEventHub.Repositories
{
    public class LocationRepository : BaseRepository<Location>, ILocationRepository
    {
        public LocationRepository(IGenericRepository<Location> repo) : base(repo)
        {

        }

        public Location GetLocationByName(string name)
        {
            return _repo.Get(x => x.Name.Equals(name)).SingleOrDefault();
        }
    }

    public interface ILocationRepository : IBaseRepository<Location>
    {
        Location GetLocationByName(string name);
    }
}