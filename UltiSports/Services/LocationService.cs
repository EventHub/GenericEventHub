using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UltiSports.Infrastructure;
using UltiSports.Models;

namespace UltiSports.Services
{
    public class LocationService : BaseService<Location>, ILocationService
    {
        private ILocationRepository _repo;

        public LocationService(ILocationRepository locationDb) : base(locationDb)
        {
            _repo = locationDb;
        }

        public IEnumerable<Location> GetAllActiveLocations()
        {
            return _repo.GetActiveLocations();
        }
    }

    public interface ILocationService : IBaseService<Location>
    {
        IEnumerable<Location> GetAllActiveLocations();
    }
}