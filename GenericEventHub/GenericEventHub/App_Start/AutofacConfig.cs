using Autofac;
using Autofac.Integration.WebApi;
using GenericEventHub.Authorization;
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
            var assembly = Assembly.GetExecutingAssembly();

            builder.RegisterApiControllers(assembly);

            builder.RegisterType<GenericEventHubDb>().As<GenericEventHubDb>().InstancePerApiRequest();

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>));

            builder.RegisterAssemblyTypes(assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assembly).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces();

            var container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);

            config.DependencyResolver = resolver;
        }
    }
}