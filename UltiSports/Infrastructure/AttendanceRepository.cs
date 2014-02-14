using System;
using System.Collections.Generic;
using System.Linq;
using UltiSports.Models;

namespace UltiSports.Infrastructure
{
    public class AttendanceRepository : BaseRepository<Attendance>, IAttendanceRepository
    {
        public AttendanceRepository(IGenericRepository<Attendance> repo)
            : base(repo)
        {

        }

        public bool IsAttending(Player player, Event ev)
        {
            bool result = false;
            var attendee = _repo.Get(x => x.Player.Username.Equals(player.Username) && x.Event.Id.Equals(ev.Id)).SingleOrDefault();
            if (attendee != null)
                result = true;

            return result;
        }

        public Attendance GetWherePlayerAndEvent(Player player, Event ev)
        {
            return _repo.Get(x => x.Player.Name.Equals(player.Name) && x.Event.Id.Equals(ev.Id)).SingleOrDefault();
        }

        public IEnumerable<Attendance> GetWhereEvent(int p)
        {
            return _repo.Get(x => x.Event.Id.Equals(p));
        }

        public int GetPlayerAttendanceForActivity(Activity activity, Player player, DateTime since)
        {
            var count = _repo.Get(x =>
                x.Player.Username.Equals(player.Username) &&
                x.Event.Activity.Name.Equals(activity.Name) &&
                x.Event.Time > since).Count();

            return count;
            /*return player.Attendance.FindAll(
                att => att.Event.Activity.Name == activity.Name
                && att.Event.Time > since).Count;*/
        }
    }

    public interface IAttendanceRepository : IBaseRepository<Attendance>
    {
        int GetPlayerAttendanceForActivity(UltiSports.Models.Activity activity, UltiSports.Models.Player player, DateTime since);
        bool IsAttending(Player player, Event ev);
        Attendance GetWherePlayerAndEvent(Player player, Event ev);
        IEnumerable<Attendance> GetWhereEvent(int p);
    }
}