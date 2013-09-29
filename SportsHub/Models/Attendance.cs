using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportsHub.Models
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }
        public virtual Event Event { get; set; }
        public virtual Player Player { get; set; }
    }
}