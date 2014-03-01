using Autofac;
using Autofac.Integration.WebApi;
using GenericEventHub.Infrastructure;
using GenericEventHub.Repositories;
using GenericEventHub.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace GenericEventHub.App_Start
{
    public class AutofacConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<GenericEventHubDb>().As<GenericEventHubDb>();

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>));

            builder.RegisterType<ActivityRepository>().As<IActivityRepository>();
            builder.RegisterType<EventRepository>().As<IEventRepository>();
            builder.RegisterType<GuestRepository>().As<IGuestRepository>();
            builder.RegisterType<LocationRepository>().As<ILocationRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();

            builder.RegisterType<ActivityService>().As<IActivityService>();
            builder.RegisterType<EventService>().As<IEventService>();
            builder.RegisterType<GuestService>().As<IGuestService>();
            builder.RegisterType<LocationService>().As<ILocationService>();
            builder.RegisterType<UserService>().As<IUserService>();

            var container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);

            config.DependencyResolver = resolver;
        }
    }
}