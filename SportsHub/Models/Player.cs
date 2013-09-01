using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportsHub.Models
{
    public class Player
    {
        [Key]
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool isActive { get; set; }
        public List<Attendance> Attendance { get; set; }
        public List<Activity> SportsManaged { get; set; }
        public List<Message> Messages { get; set; }
    }
}