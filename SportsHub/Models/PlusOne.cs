using System.ComponentModel.DataAnnotations;

namespace SportsHub.Models
{
    public class PlusOne
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Attendance Attendance { get; set; }
    }
}