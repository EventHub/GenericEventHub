﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericEventHub.Models
{
    public class Location : Entity
    {
        public int LocationID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public override int GetID()
        {
            return LocationID;
        }
    }
}