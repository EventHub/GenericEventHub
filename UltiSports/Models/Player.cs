using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Services.Description;

namespace UltiSports.Models
{
    public class Player
    {
        [Key]
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool isActive { get; set; }
        public virtual List<Attendance> Attendance { get; set; }
        public virtual List<Activity> SportsManaged { get; set; }
        public virtual List<Message> Messages { get; set; }
    }
}