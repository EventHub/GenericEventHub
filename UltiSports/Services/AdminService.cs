using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UltiSports.HelperMethods;
using UltiSports.Infrastructure;
using UltiSports.Models;

namespace UltiSports.Services
{
    public class AdminService : UltiSports.Services.IAdminService
    {
        private IEventRepository _eventDb;
        private IActivityRepository _activityDb;
        private IPlayerRepository _playerDb;
        private ILocationRepository _locationDb;
        private List<string> Admins = DataHelper.Admins();
        private IEmailService _emailServices;

        public AdminService(IEventRepository eventDb,
            IActivityRepository activityDb,
            IPlayerRepository playerDb,
            ILocationRepository locationDb,
            IEmailService emailService)
        {
            _eventDb = eventDb;
            _activityDb = activityDb;
            _playerDb = playerDb;
            _locationDb = locationDb;
            _emailServices = emailService;
        }

        public IEnumerable<Activity> GetAllActivites()
        {
            return _activityDb.GetAll();
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return _playerDb.GetAll();
        }

        public IEnumerable<Location> GetAllLocations()
        {
            return _locationDb.GetAll();
        }

        public IEnumerable<Event> GetEventsFor(string dayOfWeek, string name)
        {
            var error = string.Empty;
            Player user = _playerDb.GetPlayerByUsername(name);
            var activitiesOfTheDay = _activityDb.GetActivitiesFor(dayOfWeek);
            return _eventDb.GetEventsFor(dayOfWeek);
        }
    }

    public interface IAdminService
    {
        System.Collections.Generic.IEnumerable<UltiSports.Models.Activity> GetAllActivites();
        System.Collections.Generic.IEnumerable<UltiSports.Models.Location> GetAllLocations();
        System.Collections.Generic.IEnumerable<UltiSports.Models.Player> GetAllPlayers();
        System.Collections.Generic.IEnumerable<UltiSports.Models.Event> GetEventsFor(string dayOfWeek, string name);
    }
}