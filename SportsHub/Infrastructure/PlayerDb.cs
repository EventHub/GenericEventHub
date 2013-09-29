using System;
using System.Collections.Generic;
using SportsHub.Models;
using System.Linq;

namespace SportsHub.Infrastructure
{
    public class PlayerDb : DatabaseServices
    {
        private const string AddPlayerSuccessMessage = "Player Added successfully";
        private const string AddPlayerFailureMessage = "Player already exists";

        private const string RemovePlayerSuccessMessage = "Player Removed successfully";
        private const string RemovePlayerFailureMessage = "Player does not exist";

        /// <summary>
        /// This creates a player for the current user.
        /// </summary>
        /// <param name ="player">The player that will be created. NOTE: Windows username must be received inside the player object.</param>
        public string RegisterPlayer(Player newPlayer)
        {
            var result = string.Empty;
            newPlayer.Attendance = new List<Attendance>();
            newPlayer.Messages = new List<Message>();

            newPlayer.SportsManaged = new List<Activity>();
            try
            {
                AddEntity(newPlayer);
                result = AddPlayerSuccessMessage;
            }
            catch (Exception)
            {
                result = AddPlayerFailureMessage;
                throw;
            }

            return result;
        }

        public string InactivatePlayer(string username) 
        {
            if (!PlayerExists(username)) 
            {
                Player playerToRemove = _Db.Player.FirstOrDefault(x => x.Username.Equals(username));
                playerToRemove.isActive = false;

                UpdateEntity(playerToRemove);

                return RemovePlayerSuccessMessage;
            }

            return RemovePlayerFailureMessage;
        }

        public bool PlayerActive(string username) 
        {
            Player dbPlayer = _Db.Player.FirstOrDefault(x => x.Username.Equals(username));
            return dbPlayer != null && dbPlayer.isActive;
        }

        public bool PlayerExists(string username)
        {
            return _Db.Player.FirstOrDefault(x => x.Username.Equals(username)) != null;
        }

        /// <summary>
        /// Returns a single player with the specified name.
        /// </summary>
        /// <param name ="username">
        /// The username of the player to be retrieved.
        /// </param>
        internal Player GetPlayerByUsername(string username)
        {
            return _Db.Player.SingleOrDefault(player => player.Username == username);
        }

        internal List<Player> GetAllPlayers()
        {
            return _Db.Player.ToList();
        }

        internal bool isARegisteredPlayer(string username)
        {
            bool result = false;
            var allPlayers = GetPlayerByUsername(username);

            if (allPlayers != null)
                result = true;

            return result;
        }
    }
}