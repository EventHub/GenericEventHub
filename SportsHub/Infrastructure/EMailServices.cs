using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace SportsHub.Infrastructure
{
    public static class EMailServices
    {
        public static void SendEmail(List<string> message)
        {
            PlayerDb playerDb = new PlayerDb();
            EventDb eventDb = new EventDb();

            var allEvents = eventDb.AllEvents;
            var allPlayers = playerDb.GetAllPlayers();

            MailMessage mail = new MailMessage();

            foreach (var player in allPlayers)
            {
                mail.To.Add(player.Email);
            }
            mail.From = new MailAddress("ultievents@gmail.com");
            mail.Subject = "Ultimate Software - You're invited!";

            string body = "Hello,\n\nThis is a message from Ultimate Software's event hub. You have been invited to an after hour event today," +
                          " go to http://josealw7x6530 to stay updated on the event's status and talk with the other guests!\n\nSee you on the field!\n\nToday's Events:\n\n";

            foreach (var ev in allEvents)
            {
                if (ev.Time.Date == DateTime.Today.Date)
                {
                    body += ev.Activity.Name;
                    body += "\n";
                }
            }
            mail.Body = body;
            mail.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587; //587 is when program is running from Visual Studio, must be 25 at deployement.
            smtp.Credentials = new System.Net.NetworkCredential("ultievents@gmail.com", "usa-canu");
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}