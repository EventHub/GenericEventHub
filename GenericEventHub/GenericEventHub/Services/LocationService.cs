using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GenericEventHub.Models;
using GenericEventHub.Repositories;

namespace GenericEventHub.Services
{
    public class LocationService : BaseService<Location>, ILocationService
    {
        private ILocationRepository _repo;

        public LocationService(ILocationRepository repo)
            : base(repo)
        {
            _repo = repo;
        }
    }

    public interface ILocationService : IBaseService<Location>
    {

    }
}