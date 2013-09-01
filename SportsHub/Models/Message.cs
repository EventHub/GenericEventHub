using System;
using System.ComponentModel.DataAnnotations;

namespace SportsHub.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string MessageText { get; set; }
        public DateTime Time { get; set; }
        public Player Author { get; set; }
        public Event Event { get; set; }
    }
}