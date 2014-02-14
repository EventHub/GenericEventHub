using System;
using System.Collections.Generic;
using System.Net.Mail;
using UltiSports.Infrastructure;
using UltiSports.Models;

namespace UltiSports.Services
{
    public class EmailService : IEmailService
    {
        private IPlayerRepository _playerDb;
        private IEventRepository _eventDb;
        private IAttendanceRepository _attendanceDb;

        public EmailService(IPlayerRepository playerDb, IEventRepository eventDb, IAttendanceRepository attendanceDb)
        {
            _playerDb = playerDb;
            _eventDb = eventDb;
            _attendanceDb = attendanceDb;
        }

        private string _webAddress = "http://localhost:4634";

        private void SendEmail(IEnumerable<Player> receipents, string subject, string body)
        {
            var mail = new MailMessage();

            foreach (var r in receipents)
            {
                mail.To.Add(r.Email);
            }

            var from = "ultievents@gmail.com";
            mail.From = new MailAddress(from);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587; //587 is when program is running from Visual Studio, must be 25 at deployement.
            smtp.Credentials = new System.Net.NetworkCredential("ultievents@gmail.com", "usa-canu");
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }

        public void SendFirstEmail(string dayOfWeek)
        {
            var allEvents = _eventDb.GetEventsFor(dayOfWeek);
            var allPlayers = _playerDb.GetAll();

            var subject = "Ultimate Software - You're invited!";

            string body = String.Format("Hello,\n\nThis is a message from Ultimate Software's event hub. You have been invited to an after hour event today," +
                          " go to {0} to stay updated on the event's status and talk with the other guests!\n\nSee you on the field!\n\nToday's Events:\n\n", _webAddress);

            foreach (var ev in allEvents)
            {
                if (ev.Time.Date == DateTime.Today.Date)
                {
                    body += ev.Activity.Name + String.Format(" Join: {0}/Attendance/AddPlayer/{1}", _webAddress, ev.Id);
                    body += "\n";
                }
            }

            SendEmail(allPlayers, subject, body);
        }

        public void SendFinalEmail(string dayOfWeek)
        {
            var allEvents = _eventDb.GetEventsFor(dayOfWeek);
            List<Player> players;
            foreach (var ev in allEvents)
            {
                var ats = _attendanceDb.GetWhereEvent(ev.Id);
                players = new List<Player>();
                foreach (var a in ats) 
                {
                    players.Add(a.Player);
                }
                
                var subject = String.Format("{0} Verdict: {1}", ev.Activity.Name, "It's on!");

                var body = "See you guys out there!";

                SendEmail(players, subject, body);
            }
        }
    }

    public interface IEmailService
    {
        void SendFirstEmail(string dayOfWeek);
        void SendFinalEmail(string dayOfWeek);
    }
}