using APerepechko.HangMan.Logic.Services;
using Elmah.Contrib.WebApi;
using FluentValidation.WebApi;
using GamePortal.Web.Api.Middleware;
using IdentityServer3.AccessTokenValidation;
using IdentityServer3.AspNetIdentity;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.InMemory;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using NSwag.AspNet.Owin;
using Owin;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

[assembly: OwinStartup(typeof(GamePortal.Web.Api.Startup))]

namespace GamePortal.Web.Api
{
    public class Startup
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



            var provide = new CorsPolicyProvider();
            provide.PolicyResolver = ctx => Task.FromResult(new System.Web.Cors.CorsPolicy { AllowAnyHeader = true, AllowAnyMethod = true, AllowAnyOrigin = true });

            app.UseCors(new Microsoft.Owin.Cors.CorsOptions { PolicyProvider = provide });

            app.UseStaticFiles();

            app.UseSwagger(typeof(Startup).Assembly).UseSwaggerUi3(settings => settings.ServerUrl = "http://demovm:50698");



            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
            {
                ClientId = "740546789549-8v4dr8v2jibtj963r7po5icmkr6up4ja.apps.googleusercontent.com",
                ClientSecret = "Ysrz4Odz15usgJVKPzX5BRTR",
                AuthenticationType = "MyGoogle"
            });

            app.Map("/login/google", b => b.Use<GoogleAuthMiddleware>());


            LoadIdentityServer(app,kernel);
            
         


         


             
            //  AddHangmanSecurity(app, kernel);
            //app.MapSignalR(//path:"/signalr"  , по умолчанию заданный путь
            //    configuration:  new HubConfiguration { 
            //    EnableDetailedErrors = true,
            //    EnableJSONP = true
            //});




            app.UseNinjectMiddleware(() => kernel).UseNinjectWebApi(config);
        }



        public static void LoadIdentityServer(IAppBuilder app, IKernel kernel)
        {

            IdentityServerServiceFactory factory = new IdentityServerServiceFactory();
            var client = new Client()
            {
                ClientId = "HangmanClient",
                ClientSecrets = new List<Secret>() { new Secret("secret".Sha256()) },
                AllowAccessToAllScopes = true,
                ClientName = "Hangman Client",
                Flow = Flows.AuthorizationCode,
                RedirectUris = new List<string>() { "https://localhost:5555" , "https://localhost:4200/index.html" , "http://localhost:50698" }
            };
            var user = new InMemoryUser()
            {
                Username = "qwe",
                Password = "qwe1423",
                Subject = "123-123-123",
                //Claims = new[] { new Claim("api-version", "1") }
            };

            factory.UseInMemoryScopes(StandardScopes.All.Append(new Scope()
            {
                Name = "api",
                Type = ScopeType.Identity,
                //Claims = new List<ScopeClaim>{new ScopeClaim("api-version", true)}
            }))
                .UseInMemoryClients(new[] { client })
                .UseInMemoryUsers(new List<InMemoryUser>() { user });


            factory.UserService =
                new Registration<IdentityServer3.Core.Services.IUserService>
                (new AspNetIdentityUserService<IdentityUser, string>(kernel.Get<UserManager<IdentityUser>>()));

            app.UseIdentityServer(new IdentityServerOptions
            {
                EnableWelcomePage = true,
#if DEBUG
                RequireSsl = false,
#endif
                LoggingOptions = new LoggingOptions
                {
                    EnableHttpLogging = true,
                    EnableKatanaLogging = true,
                    EnableWebApiDiagnostics = true,
                    WebApiDiagnosticsIsVerbose = true
                },
                SiteName = "Hangman",
                Factory = factory,
                SigningCertificate = LoadCertificate()
            })
            .UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "https://localhost:44307",
                ClientId = "HangmanClient",
                ClientSecret = "secret",
                RequireHttps = false,
                ValidationMode = ValidationMode.Local,
                IssuerName = "https://localhost:44307",
                SigningCertificate = LoadCertificate(),
                ValidAudiences = new[] { "https://localhost:44307/resources" }
            });
        }


        private static X509Certificate2 LoadCertificate()
        {
            return
                new X509Certificate2(
                fileName: Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\Sertificate\idsrv3test.pfx")
                , password: "1423");
        }


        public static IAppBuilder AddHangmanSecurity(IAppBuilder app, IKernel kernel)
        {




            return app;
        }


    }
}
