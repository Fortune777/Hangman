using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Owin;
using Elmah.Contrib.WebApi;
using System.Web.Http.ExceptionHandling;
using FluentValidation.WebApi;
using Ninject;
using APerepechko.HangMan.Logic.Services;
//using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Serilog;
using System.IO;
using System.Reflection;
using NSwag.AspNet.Owin;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using System.Diagnostics;
using System.Web.Http.Owin;

[assembly: OwinStartup(typeof(GamePortal.Web.Api.StartUp))]

namespace GamePortal.Web.Api
{
    public class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            var config = new HttpConfiguration();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Services.Replace(typeof(IExceptionLogger), new ElmahExceptionLogger());

            //если ошибка loop newtonsoft -помогает узнать какой ответ пришел и какую переменную смотреть
            //config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            var kernel = new StandardKernel();
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var logger = new LoggerConfiguration()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.File(Path.Combine(path, "log.txt"))
                .Enrich.WithHttpRequestType()
                .Enrich.WithWebApiControllerName()
                .Enrich.WithWebApiActionName() // закончили настройку
                .MinimumLevel.Verbose()
                .CreateLogger(); // создать логер




            kernel.Bind<ILogger>().ToConstant(logger);
            kernel.Load(new LogicDIModule());

            FluentValidationModelValidatorProvider.Configure(config, opt =>
            {
                opt.ValidatorFactory = new CustomValidatorFactory(kernel);
            });

           

            app.UseSwagger(typeof(StartUp).Assembly).UseSwaggerUi3()
                .UseNinjectMiddleware(() => kernel).UseNinjectWebApi(config);




        }
    }
}
