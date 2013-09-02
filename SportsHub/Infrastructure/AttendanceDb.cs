using SportsHub.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
            if (!IsUserAttendingEvent(ev, player)) 
            {
                Attendance attendance = new Attendance()
                {
                    Event = ev,
                    Player = player,
                    PlusOnes = new List<PlusOne>()
                };

                this.AddEntity(attendance);
            }

            return "User is already attending event";
        }

        public bool IsUserAttendingEvent(Event ev, Player player) 
        {
            //var attendances = from att in _Db.Attendance
            //                  where
            //                      att.Event.Equals(ev) &&
            //                      att.Player.Equals(player)
            //                  select att;
            bool result = false;
            var eventoToCheckIn = _Db.Event.SingleOrDefault(evento => evento.Id == ev.Id);
            var attendanceInEventToCheck = eventoToCheckIn.Attendees;

            if (attendanceInEventToCheck != null && attendanceInEventToCheck.Count > 0)
            {
                var x = from y in attendanceInEventToCheck
                        where y.Player.Username == player.Username
                        select y;
                result = true;
            }
            //return attendances.Count() > 0;
            return result;
        }
    }
}