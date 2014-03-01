using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GenericEventHub.Models;
using GenericEventHub.Repositories;

namespace GenericEventHub.Services
{
    public class GuestService : BaseService<Guest>, IGuestService
    {
        private IGuestRepository _repo;

        public GuestService(IGuestRepository repo)
            : base(repo)
        {
            _repo = repo;
        }
    }

    public interface IGuestService : IBaseService<Guest>
    {

    }
}