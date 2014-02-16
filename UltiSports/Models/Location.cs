using System.ComponentModel.DataAnnotations;

namespace UltiSports.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        [Display(Name = "Available")]
        public bool IsActive { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}