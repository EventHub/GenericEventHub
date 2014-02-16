using System;
using System.Collections.Generic;
using System.Linq;
using UltiSports.Models;

namespace UltiSports.Infrastructure
{
    public class PlayerRepository : BaseRepository<Player>, IPlayerRepository 
    {
        public PlayerRepository(IGenericRepository<Player> repo) : base(repo)
        {

        }

        public bool PlayerActive(string username)
        {
            Player dbPlayer = GetPlayerByUsername(username);
            return dbPlayer != null && dbPlayer.IsActive;
        }

        public bool PlayerExists(string username)
        {
            return GetPlayerByUsername(username) != null;
        }

        /// <summary>
        /// Returns a single player with the specified name.
        /// </summary>
        /// <param name ="username">
        /// The username of the player to be retrieved.
        /// </param>
        public Player GetPlayerByUsername(string username)
        {
            return _repo.GetByID(username);
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return _repo.Get();
        }

        public bool IsARegisteredPlayer(string username)
        {
            return PlayerExists(username);
        }
    }

    public interface IPlayerRepository : IBaseRepository<Player>
    {
        System.Collections.Generic.IEnumerable<UltiSports.Models.Player> GetAllPlayers();
        UltiSports.Models.Player GetPlayerByUsername(string username);
        bool IsARegisteredPlayer(string username);
        bool PlayerActive(string username);
        bool PlayerExists(string username);
    }
}