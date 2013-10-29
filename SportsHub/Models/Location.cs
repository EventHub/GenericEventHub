using System.ComponentModel.DataAnnotations;

namespace SportsHub.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}