using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportsHub.Models
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }
        public Event Event { get; set; }
        public Player Player { get; set; }
        public List<PlusOne> PlusOnes { get; set; }
    }
}