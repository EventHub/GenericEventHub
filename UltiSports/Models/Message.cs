using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UltiSports.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string MessageText { get; set; }
        public DateTime Time { get; set; }
        [ForeignKey("Author")]
        public string AuthorName { get; set; }
        public virtual Player Author { get; set; }
        public int EventId { get; set; }
        public virtual Event Event { get; set; }
    }

    public class MessageDTO
    {
        public MessageDTO(Message message)
        {
            this.Author = message.Author.Name;
            this.MessageText = message.MessageText;
            this.Time = message.Time;
            this.EventId = message.Event.Id;
        }

        public string Author { get; set; }
        public string MessageText { get; set; }
        public DateTime Time { get; set; }
        public int EventId { get; set; }
    }
}