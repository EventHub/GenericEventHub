using SportsHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SportsHub.Infrastructure
{
    public class AttendanceDb : DatabaseServices
    {
        public int GetPlayerAttendanceForActivity(Activity activity, Player player, DateTime since) 
        {
            var attendances = from att in _Db.Attendance where
                                  att.Event.ActivityName == activity &&
                                  att.Player == player &&
                                  att.Event.Time > since
                              select att;

            return attendances.Count();
        }

        /// <summary>
        /// This creates an attendance record for the given player in the specified event.
        /// </summary>
        /// <param name ="ev">The event to attend.</param>
        /// <param name ="player">The player whose attendance record is going to be created.</param>
        public string AttendEvent(Event ev, Player player)
        {
            var result = String.Empty;
            if (!IsUserAttendingEvent(ev, player)) 
            {
                Attendance attendance = new Attendance()
                {
                    Event = ev,
                    Player = player,
                    PlusOnes = new List<PlusOne>()
                };

                AddEntity(attendance);
                result = "User joined event!";
            }
            else
            {
                result = "User is already attending event";
            }

            return result;
        }

        /// <summary>
        /// If there is no record of attendance or attendance record is equal or less than cero it returns false.
        /// </summary>
        /// <param name ="ev">The event to attend.</param>
        /// <param name ="player">The player whose attendance record is going to be created.</param>
        public bool IsUserAttendingEvent(Event ev, Player player) 
        {
            bool result = false;
            var attendanceInEventToCheck = ev.Attendees;

            if (attendanceInEventToCheck != null && attendanceInEventToCheck.Count > 0)
            {
                var x = from y in attendanceInEventToCheck
                        where y.Player.Username == player.Username
                        select y;
                result = true;
            }

            return result;
        }
    }
}