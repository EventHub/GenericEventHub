using System.Collections.Generic;
using UltiSports.Models;

namespace UltiSports.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using UltiSports.Infrastructure;

    internal sealed class Configuration : DbMigrationsConfiguration<UltiEventsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(UltiEventsContext context)
        {
            var regionalPark = new Location()
            {
                Address = "20200 Saddle Club Rd  Weston, FL 33327",
                Name = "Regional Park"
            };

            var tequestaTracePark = new Location()
            {
                Address = "1800 Indian Trace, Weston, FL 33326",
                Name = "Tequesta Trace Park"
            };

            var cyclingRoute = new Location()
            {
                Address = "All Over Weston",
                Name = "Weston Bike Lanes"
            };

            var runClub = new Location()
            {
                Address = "1675 Market St  Weston, FL 33326",
                Name = "Weston Town Center"
            };


            context.Activity.AddOrUpdate(
                new Activity
                {
                    Name = "Frisbee",
                    DayOfTheWeek = DayOfWeek.Monday.ToString(),
                    Managers = new List<Player>(),
                    PreferredLocation = regionalPark,
                    PreferredTime = new TimeSpan(17, 40, 0),
                    RecommendedNumberOfPlayers = 10,
                    RequiredNumberOfPlayers = 6
                },
                new Activity
                {
                    Name = "Basketball",
                    DayOfTheWeek = DayOfWeek.Tuesday.ToString(),
                    Managers = new List<Player>(),
                    PreferredLocation = tequestaTracePark,
                    PreferredTime = new TimeSpan(17, 40, 0),
                    RecommendedNumberOfPlayers = 10,
                    RequiredNumberOfPlayers = 6
                },
                new Activity
                {
                    Name = "Cycling",
                    DayOfTheWeek = DayOfWeek.Sunday.ToString(),
                    Managers = new List<Player>(),
                    PreferredLocation = cyclingRoute,
                    PreferredTime = new TimeSpan(17, 40, 0),
                    RecommendedNumberOfPlayers = 10,
                    RequiredNumberOfPlayers = 6
                },
                new Activity
                {
                    Name = "Run Club",
                    DayOfTheWeek = DayOfWeek.Wednesday.ToString(),
                    Managers = new List<Player>(),
                    PreferredLocation = runClub,
                    PreferredTime = new TimeSpan(17, 40, 0),
                    RecommendedNumberOfPlayers = 10,
                    RequiredNumberOfPlayers = 6
                },
                new Activity
                {
                    Name = "Volleyball",
                    DayOfTheWeek = DayOfWeek.Thursday.ToString(),
                    Managers = new List<Player>(),
                    PreferredLocation = regionalPark,
                    PreferredTime = new TimeSpan(17, 40, 0),
                    RecommendedNumberOfPlayers = 10,
                    RequiredNumberOfPlayers = 6
                },
                new Activity
                {
                    Name = "Football",
                    DayOfTheWeek = DayOfWeek.Thursday.ToString(),
                    Managers = new List<Player>(),
                    PreferredLocation = tequestaTracePark,
                    PreferredTime = new TimeSpan(17, 40, 0),
                    RecommendedNumberOfPlayers = 10,
                    RequiredNumberOfPlayers = 6
                }
            );
        }
    }
}
