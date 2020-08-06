using FluentValidation.WebApi;
using System.Web.Http;
using Elmah.Contrib.WebApi;
using System.Web.Http.ExceptionHandling;


namespace GamePortal.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

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

            FluentValidationModelValidatorProvider.Configure(config, opt =>
            {
                opt.ValidatorFactory = new CustomValidatorFactory(config.DependencyResolver);
            });
        }
    }
}
