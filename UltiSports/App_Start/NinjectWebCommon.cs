[assembly: WebActivator.PreApplicationStartMethod(typeof(UltiSports.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(UltiSports.App_Start.NinjectWebCommon), "Stop")]

namespace UltiSports.App_Start
{
    using Microsoft.AspNet.SignalR;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Syntax;
    using Ninject.Web.Common;
    using Ninject.Web.Mvc.FilterBindingSyntax;
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Dependencies;
    using System.Web.Mvc;
    using UltiSports.Filters;
    using UltiSports.Infrastructure;
    using UltiSports.Models;
    using UltiSports.Services;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);

            // Install our Ninject-based IDependencyResolver into the Web API config
            GlobalConfiguration.Configuration.DependencyResolver =
                    new NinjectDependencyResolver(kernel);

            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            // Register context
            kernel.Bind<UltiEventsContext>().ToSelf().InRequestScope();

            // Register generic repositories
            kernel.Bind<IGenericRepository<Activity>>().To
                <GenericRepository<Activity>>();
            kernel.Bind<IGenericRepository<Attendance>>().To
                <GenericRepository<Attendance>>();
            kernel.Bind<IGenericRepository<Event>>().To
                <GenericRepository<Event>>();
            kernel.Bind<IGenericRepository<Location>>().To
                <GenericRepository<Location>>();
            kernel.Bind<IGenericRepository<Message>>().To
                <GenericRepository<Message>>();
            kernel.Bind<IGenericRepository<Player>>().To
                <GenericRepository<Player>>();
            kernel.Bind<IGenericRepository<PlusOne>>().To
                <GenericRepository<PlusOne>>();
            
            // Register repositories
            kernel.Bind<IActivityRepository>().To<ActivityRepository>();
            kernel.Bind<IAttendanceRepository>().To<AttendanceRepository>();
            kernel.Bind<IEventRepository>().To<EventRepository>();
            kernel.Bind<ILocationRepository>().To<LocationRepository>();
            kernel.Bind<IMessageRepository>().To<MessageRepository>();
            kernel.Bind<IPlayerRepository>().To<PlayerRepository>();
            kernel.Bind<IPlusOneRepository>().To<PlusOneRepository>();

            // Register services
            kernel.Bind<IActivityService>().To<ActivityService>();
            kernel.Bind<IAdminService>().To<AdminService>();
            kernel.Bind<IAttendanceService>().To<AttendanceService>();
            kernel.Bind<IEmailService>().To<EmailService>();
            kernel.Bind<IEventService>().To<EventService>();
            kernel.Bind<ILocationService>().To<LocationService>();
            kernel.Bind<IMessageService>().To<MessageService>();
            kernel.Bind<IPlayerService>().To<PlayerService>();

            kernel.BindFilter<AdminAuthentication>(FilterScope.Controller, 0).WhenControllerHas<AdminAuthentication>();
        }        
    }

    public class NinjectDependencyScope : IDependencyScope
    {
        IResolutionRoot resolver;
        public NinjectDependencyScope(IResolutionRoot resolver)
        {
            this.resolver = resolver;
        }
        public object GetService(Type serviceType)
        {
            if (resolver == null)
                throw new ObjectDisposedException("this", "This scope has been disposed");
            return resolver.TryGet(serviceType);
        }
        public System.Collections.Generic.IEnumerable<object> GetServices(Type serviceType)
        {
            if (resolver == null)
                throw new ObjectDisposedException("this", "This scope has been disposed");
            return resolver.GetAll(serviceType);
        }
        public void Dispose()
        {
            IDisposable disposable = resolver as IDisposable;
            if (disposable != null)
                disposable.Dispose();
            resolver = null;
        }
    }

    public class NinjectDependencyResolver : NinjectDependencyScope, System.Web.Http.Dependencies.IDependencyResolver
    {
        IKernel kernel;
        public NinjectDependencyResolver(IKernel kernel)
            : base(kernel)
        {
            this.kernel = kernel;
        }
        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(kernel.BeginBlock());
        }
    }
}
