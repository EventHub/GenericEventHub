using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UltiSports.Models
{
    public class PlusOne
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int EventId { get; set; }
        public virtual Event Event { get; set; }
        [ForeignKey("Host")]
        public string HostName { get; set; }
        public virtual Player Host { get; set; }
    }
}