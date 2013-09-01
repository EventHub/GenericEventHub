namespace SportsHub.Migrations
{
    using SportsHub.Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<SportsHub.Infrastructure.SportsHubDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SportsHub.Infrastructure.SportsHubDb context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Activity.AddOrUpdate(
                new Activity 
                { 
                    Name = "Frisbee",
                    DayOfTheWeek = DayOfWeek.Monday.ToString(),
                    Managers = new List<Player>(),
                    PreferredLocation = null,
                    PreferredTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 40, 0),
                    RecommendedNumberOfPlayers = 10,
                    RequiredNumberOfPlayers = 6
                },
                new Activity
                {
                    Name = "Basketball",
                    DayOfTheWeek = DayOfWeek.Tuesday.ToString(),
                    Managers = new List<Player>(),
                    PreferredLocation = null,
                    PreferredTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 40, 0),
                    RecommendedNumberOfPlayers = 10,
                    RequiredNumberOfPlayers = 6
                },
                new Activity
                {
                    Name = "Run Club",
                    DayOfTheWeek = DayOfWeek.Wednesday.ToString(),
                    Managers = new List<Player>(),
                    PreferredLocation = null,
                    PreferredTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 40, 0),
                    RecommendedNumberOfPlayers = 10,
                    RequiredNumberOfPlayers = 6
                },
                new Activity
                {
                    Name = "Volleyball",
                    DayOfTheWeek = DayOfWeek.Thursday.ToString(),
                    Managers = new List<Player>(),
                    PreferredLocation = null,
                    PreferredTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 40, 0),
                    RecommendedNumberOfPlayers = 10,
                    RequiredNumberOfPlayers = 6
                },
                new Activity
                {
                    Name = "Friday's Sport",
                    DayOfTheWeek = DayOfWeek.Friday.ToString(),
                    Managers = new List<Player>(),
                    PreferredLocation = null,
                    PreferredTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 40, 0),
                    RecommendedNumberOfPlayers = 10,
                    RequiredNumberOfPlayers = 6
                }, 
                new Activity
                {
                    Name = "Saturday's Sport",
                    DayOfTheWeek = DayOfWeek.Saturday.ToString(),
                    Managers = new List<Player>(),
                    PreferredLocation = null,
                    PreferredTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 40, 0),
                    RecommendedNumberOfPlayers = 12,
                    RequiredNumberOfPlayers = 8
                }
            );
        }
    }
}
