using GenericEventHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericEventHub.DTOs
{
    public class EventUserDTO : DTO
    {
        public int UserID { get; set; }
        public string Name { get; set; }

        public EventUserDTO(User user) : base(user)
        {
            Name = user.GetName();
        }
    }
}