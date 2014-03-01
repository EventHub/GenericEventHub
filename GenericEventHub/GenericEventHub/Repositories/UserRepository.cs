using GenericEventHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericEventHub.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository 
    {
        public UserRepository(IGenericRepository<User> repo) : base(repo)
        {

        }

        public User GetUserByUsername(string username)
        {
            return _repo.GetByID(username);
        }

        public bool IsARegisteredUser(string username)
        {
            return GetUserByUsername(username) != null;
        }
    }

    public interface IUserRepository : IBaseRepository<User>
    {
        User GetUserByUsername(string username);
        bool IsARegisteredUser(string username);
    }
}