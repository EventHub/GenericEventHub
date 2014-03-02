namespace GenericEventHub.Migrations
{
    using GenericEventHub.Infrastructure;
    using GenericEventHub.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GenericEventHubDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GenericEventHubDb context)
        {
            context.Set<Location>().AddOrUpdate(new Location[] {
                new Location() {
                    Name = "Test",
                    Address = "Test test"
                }
            });
        }
    }
}
