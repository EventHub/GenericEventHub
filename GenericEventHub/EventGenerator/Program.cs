using GenericEventHub.Infrastructure;
using GenericEventHub.Models;
using GenericEventHub.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            GenericEventHubDb context = new GenericEventHubDb();

            IGenericRepository<Event> genericEventRepo = new GenericRepository<Event>(context);
            IEventRepository eventRepo = new EventRepository(genericEventRepo);

            IGenericRepository<Activity> genericActivityRepo = new GenericRepository<Activity>(context);
            IActivityRepository activityRepo = new ActivityRepository(genericActivityRepo);

            var generator = new EventGenerator(eventRepo, activityRepo);

            // Generate events for next week
            generator.CreateEventsForNextWeek();
        }
    }
}
