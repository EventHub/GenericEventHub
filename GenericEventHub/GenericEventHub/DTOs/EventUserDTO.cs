using GenericEventHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericEventHub.DTOs
{
    public class EventUserDTO
    {
        public int UserID { get; set; }
        public string Name { get; set; }

        public EventUserDTO(User user)
        {
            UserID = user.UserID;
            Name = user.Name ?? user.WindowsName;
        }
    }
}