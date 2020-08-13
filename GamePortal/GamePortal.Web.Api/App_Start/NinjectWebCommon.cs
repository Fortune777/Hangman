//[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(GamePortal.Web.Api.App_Start.NinjectWebCommon), "Start")]
//[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(GamePortal.Web.Api.App_Start.NinjectWebCommon), "Stop")]

namespace GamePortal.Web.Api.App_Start
{
    using System;
    using System.Web;
    using APerepechko.HangMan.Logic.Services;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using Serilog;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using Ninject.Web.WebApi;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
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
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var logger = new LoggerConfiguration()
                .WriteTo.Debug()
                .WriteTo.Console()
                //.WriteTo.File(Path.Combine(path, "log.txt"))
                .Enrich.WithHttpRequestType()
                .Enrich.WithWebApiControllerName()
                .Enrich.WithWebApiActionName() // закончили настройку
                .MinimumLevel.Verbose()
                .WriteTo.File(Path.Combine(path, "log.txt"))
                .CreateLogger(); // создать логер

            kernel.Bind<ILogger>().ToConstant(logger);
            kernel.Load(new LogicDIModule());
        }
    }
}