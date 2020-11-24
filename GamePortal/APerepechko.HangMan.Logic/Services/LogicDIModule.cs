using APerepechko.HangMan.Data;
using APerepechko.HangMan.Data.Profiles;
using APerepechko.HangMan.Logic.Model;
using APerepechko.HangMan.Logic.Validators;
using AutoMapper;
using FluentValidation;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using APerepechko.HangMan.Logic.Aspects;
using Ninject;
using Serilog;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using APerepechko.HangMan.Logic.Identity;
using APerepechko.HangMan.Logic.Services.Contracts;

namespace APerepechko.HangMan.Logic.Services
{
    public class LogicDIModule : NinjectModule
    {
        public override void Load()
        {
            Mapper.Initialize(cfg => cfg.AddProfiles(typeof(WordProfile)));

            var mapper = Mapper.Configuration.CreateMapper();

            this.Bind<IMapper>().ToConstant(mapper);
            this.Bind<HangmanContext>().ToSelf();
            this.Bind<IHangmanService>().ToMethod(
                ctx =>
                {
                    var service = new HangmanService(ctx.Kernel.Get<HangmanContext>(), ctx.Kernel.Get<IMapper>(), ctx.Kernel.Get<ILogger>());
                    return new ProxyGenerator().CreateInterfaceProxyWithTarget<IHangmanService>(service, new ValidationInterceptor(ctx.Kernel));
                });

            this.Bind<IValidator<WordDto>>().To<WordDtoValidators>(); // для использования другого rule set-а PostValidator
            this.Bind<IValidator<UserStatisticsDto>>().To<UserStatisticsDtoValidators>(); // для использования другого rule set-а PostValidator
            this.Bind<IValidator<ThemeDto>>().To<ThemeDtoValidators>(); // для использования другого rule set-а PostValidator
            this.Bind<IValidator<UserDto>>().To<UserDtoValidators>(); // для использования другого rule set-а PostValidator
            this.Bind<IValidator<NewUserDto>>().To<NewUserDtoValidators>(); // для использования другого rule set-а PostValidator


            //registr asp.net Identity

            this.Bind<IUserStore<IdentityUser>>().ToMethod(ctx =>
                new UserStore<IdentityUser>(ctx.Kernel.Get<HangmanContext>()));
            
            this.Bind<UserManager<IdentityUser>>().ToMethod(ctx =>
            {
                var manager = new UserManager<IdentityUser>(ctx.Kernel.Get<IUserStore<IdentityUser>>());
                manager.EmailService = new HangmanEmailService();
                manager.UserValidator = new UserValidator<IdentityUser>(manager)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = true

                };
                manager.PasswordValidator = new PasswordValidator()
                {
                    RequireDigit = false,
                    RequiredLength = 3,
                    RequireLowercase = false,
                    RequireNonLetterOrDigit = false,
                    RequireUppercase = false
                };
                manager.UserTokenProvider = new EmailTokenProvider<IdentityUser>();

                return manager;
            });

            this.Bind<IUserService>().To<UserService>();
        }
    }
}
