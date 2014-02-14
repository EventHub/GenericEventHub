using System;
using System.ComponentModel.DataAnnotations;

namespace UltiSports.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string MessageText { get; set; }
        public DateTime Time { get; set; }
        public virtual Player Author { get; set; }
        public virtual Event Event { get; set; }
    }
}