using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GenericEventHub.Models;
using GenericEventHub.Repositories;

namespace GenericEventHub.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private IUserRepository _repo;

        public UserService(IUserRepository repo)
            : base(repo)
        {
            _repo = repo;
        }
    }

    public interface IUserService : IBaseService<User>
    {

    }
}