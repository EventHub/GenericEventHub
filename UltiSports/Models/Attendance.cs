using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UltiSports.Models
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }
        public int EventId { get; set; }
        public virtual Event Event { get; set; }
        public virtual Player Player { get; set; }
    }
}