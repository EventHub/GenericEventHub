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
            return player.Attendance.FindAll(
                att => att.Event.Activity.Name == activity.Name
                && att.Event.Time > since).Count;
        }

        /// <summary>
        /// This creates an attendance record for the given player in the specified event.
        /// </summary>
        /// <param name ="ev">The event to attend.</param>
        /// <param name ="player">The player whose attendance record is going to be created.</param>
        internal string AttendEvent(Event ev, Player player)
        {
            var result = String.Empty;
            if (!IsUserAttendingEvent(ev, player)) 
            {
                Attendance attendance = new Attendance()
                {
                    Event = ev,
                    Player = player,
                };

                AddEntity(attendance);
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
        internal string LeaveEvent(Event eventToAttend, Player player)
        {
            var result = String.Empty;
            if (IsUserAttendingEvent(eventToAttend, player))
            {
                Attendance playersAttendance = eventToAttend.Attendees.SingleOrDefault(att => att.Player.Username == player.Username);
                eventToAttend.Attendees.Remove(playersAttendance);
                this.DeleteEntity(playersAttendance);
                // this.UpdateEntity(eventToAttend);
            }
            else
            {
                result = "User has already left the event.";
            }
            return result;
        }

        /// <summary>
        /// Add a plus one for the current player.
        /// </summary>
        /// <param name ="ev">The event to attend.</param>
        /// <param name ="player">The player whose attendance record is going to be removed for said event.</param>
        /// <param name ="guestName">The name of the new guest.</param>
        internal string AddPlusOne(Event eventToAttend, Player player, string guestName = "Guest")
        {
            var result = String.Empty;
            if (this.IsUserAttendingEvent(eventToAttend, player))
            {
                eventToAttend.PlusOnes.Add(new PlusOne(){Name = guestName, Host = player, Event = eventToAttend});
                this.UpdateEntity(eventToAttend);
            }
            else
            {
                result = "You cannot add a plus one if you have not joined this event!";
            }

            return result;
        }

        public string RemovePlusOne(Event currentEvent, PlusOne plusOneToRemove)
        {
            string result = string.Empty;
            if (this.IsPlusOneAttendingEvent(currentEvent, plusOneToRemove))
            {
                currentEvent.PlusOnes.Remove(plusOneToRemove);
                this.UpdateEntity(currentEvent);
            }
            else
            {
                result = "Plus one is no longer participating in this event.";
            }

            return result;
        }

        /// <summary>
        /// If there is no record of attendance or attendance record is equal or less than cero it returns false.
        /// </summary>
        /// <param name ="ev">The event to attend.</param>
        /// <param name ="player">The player whose attendance record is going to be created.</param>
        internal bool IsUserAttendingEvent(Event ev, Player player) 
        {
            bool result = false;
            var attendee = ev.Attendees.SingleOrDefault(att => att.Player.Username == player.Username);
            if (attendee != null)
                result = true;

            return result;
        }

        private bool IsPlusOneAttendingEvent(Event ev, PlusOne plusOne)
        {
            bool result = false;
            var foundPlusOne = ev.PlusOnes.SingleOrDefault(po => po.Id == plusOne.Id);
            if (foundPlusOne != null)
                result = true;

            return result;
        }
    }
}