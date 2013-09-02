using SportsHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsHub.Infrastructure
{
    public class PlayerDb : DatabaseServices
    {
        private const string AddPlayerSuccessMessage = "Player Added successfully";
        private const string AddPlayerFailureMessage = "Player already exists";

        private const string RemovePlayerSuccessMessage = "Player Removed successfully";
        private const string RemovePlayerFailureMessage = "Player does not exist";
        
        public string AddPlayer(Player player)
        {
            if (!PlayerExists(player.Username))
            {
                if (!PlayerActive(player.Username))
                {
                    this.AddEntity(player);

                }
                else 
                {
                    player.isActive = true;
                    this.UpdateEntity(player);
                }
                
                return AddPlayerSuccessMessage;
            }
            else
            {
                return AddPlayerFailureMessage;
            }
        }

        public string InactivatePlayer(string username) 
        {
            if (!PlayerExists(username)) 
            {
                Player playerToRemove = _Db.Player.FirstOrDefault(x => x.Username.Equals(username));
                playerToRemove.isActive = false;

                this.UpdateEntity(playerToRemove);

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
    }
}