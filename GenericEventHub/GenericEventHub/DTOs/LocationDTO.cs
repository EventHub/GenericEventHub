using GenericEventHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericEventHub.DTOs
{
    public class LocationDTO : DTO
    {
        public int LocationID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public LocationDTO(Location location) : base(location)
        {
        }
    }
}