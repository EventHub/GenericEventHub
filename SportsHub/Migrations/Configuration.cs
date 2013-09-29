using SportsHub.Infrastructure;

namespace SportsHub.Migrations
{
    using Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<SportsHubDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SportsHubDb context)
        {
            context.Activity.AddOrUpdate(
                new Activity 
                { 
                    Name = "Frisbee",
                    DayOfTheWeek = DayOfWeek.Monday.ToString(),
                    Managers = new List<Player>(),
                    PreferredLocation = new Location() { Address = "Parque", Activities = null, Name = "Tequesta Trace"},
                    PreferredTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 40, 0),
                    RecommendedNumberOfPlayers = 10,
                    RequiredNumberOfPlayers = 6
                },
                new Activity
                {
                    Name = "Basketball",
                    DayOfTheWeek = DayOfWeek.Tuesday.ToString(),
                    Managers = new List<Player>(),
                    PreferredLocation = new Location() { Address = "Park", Activities = null, Name = "Tequesta Trace" },
                    PreferredTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 40, 0),
                    RecommendedNumberOfPlayers = 10,
                    RequiredNumberOfPlayers = 6
                },
                new Activity
                {
                    Name = "Basketball 2",
                    DayOfTheWeek = DayOfWeek.Tuesday.ToString(),
                    Managers = new List<Player>(),
                    PreferredLocation = new Location() { Address = "Geschaft", Activities = null, Name = "Tequesta Trace" },
                    PreferredTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 40, 0),
                    RecommendedNumberOfPlayers = 10,
                    RequiredNumberOfPlayers = 6
                },
                new Activity
                {
                    Name = "Basketball 3",
                    DayOfTheWeek = DayOfWeek.Tuesday.ToString(),
                    Managers = new List<Player>(),
                    PreferredLocation = new Location() { Address = "Caleta", Activities = null, Name = "Tequesta Trace" },
                    PreferredTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 40, 0),
                    RecommendedNumberOfPlayers = 10,
                    RequiredNumberOfPlayers = 6
                },
                new Activity
                {
                    Name = "Run Club",
                    DayOfTheWeek = DayOfWeek.Wednesday.ToString(),
                    Managers = new List<Player>(),
                    PreferredLocation = new Location() { Address = "Caleta1", Activities = null, Name = "Tequesta Trace" },
                    PreferredTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 40, 0),
                    RecommendedNumberOfPlayers = 10,
                    RequiredNumberOfPlayers = 6
                },
                new Activity
                {
                    Name = "Volleyball",
                    DayOfTheWeek = DayOfWeek.Thursday.ToString(),
                    Managers = new List<Player>(),
                    PreferredLocation = new Location() { Address = "Caleta2", Activities = null, Name = "Tequesta Trace" },
                    PreferredTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 40, 0),
                    RecommendedNumberOfPlayers = 10,
                    RequiredNumberOfPlayers = 6
                },
                new Activity
                {
                    Name = "Friday's Sport",
                    DayOfTheWeek = DayOfWeek.Friday.ToString(),
                    Managers = new List<Player>(),
                    PreferredLocation = new Location() { Address = "Caleta3", Activities = null, Name = "Tequesta Trace" },
                    PreferredTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 40, 0),
                    RecommendedNumberOfPlayers = 10,
                    RequiredNumberOfPlayers = 6
                }, 
                new Activity
                {
                    Name = "Saturday's Sport",
                    DayOfTheWeek = DayOfWeek.Saturday.ToString(),
                    Managers = new List<Player>(),
                    PreferredLocation = new Location() { Address = "Caleta4", Activities = null, Name = "Tequesta Trace" },
                    PreferredTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 40, 0),
                    RecommendedNumberOfPlayers = 12,
                    RequiredNumberOfPlayers = 8
                }
            );
        }
    }
}
