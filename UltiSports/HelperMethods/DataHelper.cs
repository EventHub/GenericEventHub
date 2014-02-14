using System;
using System.Collections.Generic;
using System.Web.Mvc;
using UltiSports.Models;

namespace UltiSports.HelperMethods
{
    public class DataHelper
    {
        public static SelectList GetStandardTimes()
        {
            var timeList = new List<TimeSpan>
                {
                    new TimeSpan(16, 0, 0),
                    new TimeSpan(16, 30, 0),
                    new TimeSpan(17, 0, 0),
                    new TimeSpan(17, 30, 0),
                    new TimeSpan(18, 0, 0),
                    new TimeSpan(18, 30, 0),
                    new TimeSpan(19, 0, 0),
                    new TimeSpan(19, 30, 0)
                };

            return
                new SelectList(timeList);
        }

        public static SelectList GetDays()
        {
            return new SelectList(new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" });
        }

        public static SelectList GetNumberOfPlayers(int maxNumberOfPlayers)
        {
            var numOfPlayers = new int[maxNumberOfPlayers];

            for (var i = 0; i < maxNumberOfPlayers; i++)
            {
                numOfPlayers[i] = i + 1;
            }

            return new SelectList(numOfPlayers);
        }

        public static SelectList GetAllLocations(IEnumerable<Location> allLocations)
        {
            var result = new SelectList(allLocations, "Id", "Name");

            return result;
        }

        public static List<string> Admins()
        {
            var validAdmins = new List<string>();
            validAdmins.Add(@"USCORP\MichaelLo");
            validAdmins.Add(@"USCORP\JoseAl");
            validAdmins.Add(@"USCORP\MichaelR");
            validAdmins.Add(@"USCORP\KevinP");


            return validAdmins;
        }
    }
}