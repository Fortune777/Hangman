using APerepechko.HangMan.Data;
using APerepechko.HangMan.Data.Profiles;
using APerepechko.HangMan.Logic.Model;
using APerepechko.HangMan.Logic.Validators;
using APerepechko.HangMan.Model.Logic;
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
        }
    }
}
