using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UltiSports.Infrastructure;
using UltiSports.Models;

namespace UltiSports.Services
{
    public class PlayerService : BaseService<Player>, UltiSports.Services.IPlayerService
    {
        private const string AddPlayerSuccessMessage = "Player Added successfully";
        private const string AddPlayerFailureMessage = "Player already exists";

        private const string RemovePlayerSuccessMessage = "Player Removed successfully";
        private const string RemovePlayerFailureMessage = "Player does not exist";

        private IPlayerRepository _playerDb;
        private IAttendanceRepository _attendanceDb;
        private IEventRepository _eventDb;

        public PlayerService(IPlayerRepository playerDb, IAttendanceRepository attendanceDb, IEventRepository eventDb) : base(playerDb)
        {
            _playerDb = playerDb;
            _attendanceDb = attendanceDb;
            _eventDb = eventDb;
        }

        /// <summary>
        /// This creates a player for the current _user.
        /// </summary>
        /// <param name ="player">The player that will be created. NOTE: Windows username must be received inside the player object.</param>
        public string Register(Player newPlayer)
        {
            var result = string.Empty;
            newPlayer.Attendance = new List<Attendance>();
            newPlayer.Messages = new List<Message>();

            newPlayer.IsActive = true;

            newPlayer.SportsManaged = new List<Activity>();
            try
            {

                _playerDb.Insert(newPlayer);
                result = AddPlayerSuccessMessage;
            }
            catch (Exception)
            {
                result = AddPlayerFailureMessage;
                throw;
            }

            return result;
        }

        public bool IsRegistered(string username)
        {
            return _playerDb.IsARegisteredPlayer(username);
        }

        public string InactivatePlayer(string username)
        {
            if (_playerDb.PlayerExists(username))
            {
                Player playerToRemove = _playerDb.GetPlayerByUsername(username);
                playerToRemove.IsActive = false;

                _playerDb.Update(playerToRemove);

                return RemovePlayerSuccessMessage;
            }

            return RemovePlayerFailureMessage;
        }
    }

    public interface IPlayerService : IBaseService<Player>
    {
        string InactivatePlayer(string username);
        bool IsRegistered(string username);
        string Register(UltiSports.Models.Player newPlayer);
    }
}