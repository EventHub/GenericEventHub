using GenericEventHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericEventHub.DTOs
{
    public class UserDTO : DTO
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string EMail { get; set; }

        public UserDTO(User user)
            : base(user)
        {
            Name = user.GetName();
        } 
    }
}