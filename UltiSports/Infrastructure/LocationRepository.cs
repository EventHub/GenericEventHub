using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using UltiSports.Models;

namespace UltiSports.Infrastructure
{
    public class LocationRepository : BaseRepository<Location>, ILocationRepository
    {
        public LocationRepository(IGenericRepository<Location> repo) : base(repo)
        {

        }

        public IEnumerable<Location> GetAllLocations()
        {
            return _repo.Get();
        }

        public Location GetLocationById(int id)
        {
            return _repo.GetByID(id);
        }

        public Location GetLocationByName(string name)
        {
            return _repo.Get(x => x.Name.Equals(name)).SingleOrDefault();
        }

        public IEnumerable<Location> GetActiveLocations()
        {
            return _repo.Get(loc => loc.IsActive == true);
        }
    }

    public interface ILocationRepository : IBaseRepository<Location>
    {
        System.Collections.Generic.IEnumerable<UltiSports.Models.Location> GetAllLocations();
        UltiSports.Models.Location GetLocationById(int id);
        UltiSports.Models.Location GetLocationByName(string name);

        IEnumerable<Location> GetActiveLocations();
    }
}