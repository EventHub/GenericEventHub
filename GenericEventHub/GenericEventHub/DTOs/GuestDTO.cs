using GenericEventHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericEventHub.DTOs
{
    public class GuestDTO : DTO
    {
        public int GuestID { get; set; }
        public string Name { get; set; }
        public int HostID { get; set; }
        public int EventID { get; set; }

        public GuestDTO(Guest guest) : base(guest)
        {
        }
    }
}