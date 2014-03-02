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

        public bool ShouldInsertLocation(GenericEventHubDb context, Location location)
        {
            return context.Set<Location>().Count<Location>(x => x.Name == location.Name) == 0;
        }

        public bool ShouldInsertActivity(GenericEventHubDb context, Activity activity)
        {
            return context.Set<Location>().Count<Location>(x => x.Name == activity.Name) == 0;
        }

        protected override void Seed(GenericEventHubDb context)
        {

            //// Add locations
            //Location westonRegional = new Location() {
            //    LocationID = 100,
            //    Name = "Weston Regional Park",
            //    Address = "20200 Saddle Club Rd, Weston, FL 33327"
            //};

            //if (ShouldInsertLocation(context, westonRegional))
            //    context.Set<Location>().AddOrUpdate(new Location[] {
            //        westonRegional
            //    });

            //Location tequestaTracePark = new Location() {
            //    LocationID = 101,
            //    Name = "Tequesta Trace Park",
            //    Address = "600 Indian Trace, Weston, FL 33326"
            //};

            //if (ShouldInsertLocation(context, tequestaTracePark))
            //    context.Set<Location>().AddOrUpdate(new Location[] {
            //        tequestaTracePark
            //    });

            //// Add activities
            //Activity frisbee = new Activity()
            //{
            //    Name = "Frisbee",
            //    LocationID = 101,
            //    PreferredTime = new TimeSpan(17, 45, 00),
            //    DayOfWeek = "Monday"
            //};

            //if (ShouldInsertActivity(context, frisbee))
            //    context.Set<Activity>().AddOrUpdate(new Activity[] {
            //        frisbee
            //    });

            //Activity volleyball = new Activity()
            //{
            //    Name = "Volleyball",
            //    LocationID = 100,
            //    PreferredTime = new TimeSpan(17, 30, 00),
            //    DayOfWeek = "Thursday"
            //};


            //if (ShouldInsertActivity(context, volleyball))
            //    context.Set<Activity>().AddOrUpdate(new Activity[] {
            //        volleyball
            //    });
        }
    }
}
