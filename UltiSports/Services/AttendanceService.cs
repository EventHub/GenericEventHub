using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UltiSports.Infrastructure;
using UltiSports.Models;

namespace UltiSports.Services
{
    public class AttendanceService : UltiSports.Services.IAttendanceService
    {
        private IAttendanceRepository _attendanceDb;
        private IEventRepository _eventDb;
        private IPlayerRepository _playerDb;
        private IPlusOneRepository _plusOneDb;

        public AttendanceService(IAttendanceRepository attendanceDb,
            IEventRepository eventDb,
            IPlayerRepository playerDb,
            IPlusOneRepository plusOneDb)
        {
            _attendanceDb = attendanceDb;
            _eventDb = eventDb;
            _playerDb = playerDb;
            _plusOneDb = plusOneDb;
        }

        public string AddPlayer(int eventId, string name)
        {
            var result = String.Empty;
            var ev = _eventDb.GetByID(eventId);
            var player = _playerDb.GetByID(name);
            if (!_attendanceDb.IsAttending(player, ev))
            {
                Attendance attendance = new Attendance()
                {
                    Event = ev,
                    Player = player,
                };
                ev.Attendees.Add(player);
                _attendanceDb.Insert(attendance);
                result = "User joined event!";
            }
            else
            {
                result = "User is already attending event.";
            }

            return result;
        }

        /// <summary>
        /// Remove player from the Attendance list for the current event.
        /// </summary>
        /// <param name ="ev">The event to attend.</param>
        /// <param name ="player">The player whose attendance record is going to be removed for said event.</param>
        public string RemovePlayer(int eventId, string name)
        {
            var result = String.Empty; 
            var ev = _eventDb.GetByID(eventId);
            var player = _playerDb.GetByID(name);
            var attendance = _attendanceDb.GetWherePlayerAndEvent(player, ev);
            if (attendance != null) {
                ev.Attendees.Remove(player);
                ev.PlusOnes.RemoveAll(x => x.Host.Name.Equals(player.Name));
                _attendanceDb.Delete(attendance);
                result = "User removed from event.";
            }
            else
            {
                result = "User has already left the event.";
            }
            return result;
        }

        public string AddPlusOne(int eventId, string name, string guestName = "Guest")
        {
            string result = string.Empty;
            var eventToAttend = _eventDb.GetByID(eventId);
            var player = _playerDb.GetPlayerByUsername(name);
            if (_attendanceDb.IsAttending(player, eventToAttend))
            {
                var plusOne = new PlusOne() { Name = guestName, Host = player, Event = eventToAttend };
                eventToAttend.PlusOnes.Remove(plusOne);
                _plusOneDb.Insert(plusOne);
            }
            else
            {
                result = "You cannot add a plus one if you have not joined this event!";
            }

            return result;
        }

        public string RemovePlusOne(int id, int plusOneId)
        {
            string result = string.Empty;
            Event currentEvent = _eventDb.GetByID(id);
            PlusOne plusOneToRemove = _plusOneDb.GetByID(plusOneId);
            if (_plusOneDb.IsAttending(plusOneToRemove, currentEvent))
            {
                currentEvent.PlusOnes.Remove(plusOneToRemove);
                _plusOneDb.Delete(plusOneToRemove);
            }
            else
            {
                result = "Plus one is no longer participating in this event.";
            }

            return result;
        }

    }

    public interface IAttendanceService
    {
        string AddPlayer(int eventId, string name);
        string RemovePlayer(int eventId, string name);
        string AddPlusOne(int eventId, string name, string guestName = "Guest");
        string RemovePlusOne(int id, int plusOneId);
    }
}