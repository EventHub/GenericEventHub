using System.ComponentModel.DataAnnotations;

namespace SportsHub.Models
{
    public class PlusOne
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Event Event { get; set; }
        public virtual Player Host { get; set; }
    }
}