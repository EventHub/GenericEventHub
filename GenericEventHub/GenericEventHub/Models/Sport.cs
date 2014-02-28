using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericEventHub.Models
{
    public class Sport : Activity
    {
        public int RequiredNumberOfPlayers { get; set; }
        public int RecommendedNumberOfPlayers { get; set; }
    }
}