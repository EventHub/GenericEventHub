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

    public class MessageDTO
    {
        public MessageDTO(Message message)
        {
            this.Author = message.Author.Name;
            this.MessageText = message.MessageText;
            this.Time = message.Time;
        }

        public string Author { get; set; }
        public string MessageText { get; set; }
        public DateTime Time { get; set; }
    }
}