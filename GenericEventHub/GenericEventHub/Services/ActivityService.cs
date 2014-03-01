using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GenericEventHub.Models;
using GenericEventHub.Repositories;

namespace GenericEventHub.Services
{
    public class ActivityService : BaseService<Activity>, IActivityService
    {
        private IActivityRepository _repo;

        public ActivityService(IActivityRepository repo) : base(repo)
        {
            _repo = repo;
        }
    }

    public interface IActivityService : IBaseService<Activity>
    {

    }
}