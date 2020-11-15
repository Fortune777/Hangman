using APerepechko.HangMan.Logic.Model;
using Castle.DynamicProxy;
using CSharpFunctionalExtensions;
using FluentValidation;
using JetBrains.Annotations;
using Ninject;
using NullGuard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APerepechko.HangMan.Logic.Aspects
{
    [NullGuard(ValidationFlags.None)]  // для класса отключаем проверку nullguard
    class ValidationInterceptor : IInterceptor
    {
        private readonly IKernel _kernel;

        public ValidationInterceptor(IKernel kernel)
        {
            this._kernel = kernel;
        }
         
        public void Intercept(IInvocation invocation)
        {
            if(invocation.Arguments.Length == 0) { invocation.Proceed(); return; }

            if (invocation.Arguments[0] is UserStatisticsDto argStatic)
            {
                if (argStatic == null) { invocation.Proceed(); return; }

                var validator = _kernel.Get<IValidator<UserStatisticsDto>>();
                var validationResult = validator.Validate(instance: argStatic, ruleSet: "PreValidation"); // contract
                if (!validationResult.IsValid)
                {
                    invocation.ReturnValue = Result.Failure<UserStatisticsDto>(validationResult.Errors.Select(x => x.ErrorMessage).First());
                    return;
                }
            }


            if (invocation.Arguments[0] is ThemeDto argTheme)
            {
                if (argTheme == null) { invocation.Proceed(); return; }

                var validator = _kernel.Get<IValidator<ThemeDto>>();
                var validationResult = validator.Validate(instance: argTheme, ruleSet: "PreValidation"); // contract
                if (!validationResult.IsValid)
                {
                    invocation.ReturnValue = Result.Failure<ThemeDto>(validationResult.Errors.Select(x => x.ErrorMessage).First());
                    return;
                }
            }

            if (invocation.Arguments[0] is UserDto argUser)
            {
                if (argUser == null) { invocation.Proceed(); return; }

                var validator = _kernel.Get<IValidator<UserDto>>();
                var validationResult = validator.Validate(instance: argUser, ruleSet: "PreValidation"); // contract
                if (!validationResult.IsValid)
                {
                    invocation.ReturnValue = Result.Failure<UserDto>(validationResult.Errors.Select(x => x.ErrorMessage).First());
                    return;
                }
            }


            if (invocation.Arguments[0] is NewUserDto argNewUser)
            {
                if (argNewUser == null) { invocation.Proceed(); return; }

                var validator = _kernel.Get<IValidator<NewUserDto>>();
                var validationResult = validator.Validate(instance: argNewUser, ruleSet: "PreValidation"); // contract
                if (!validationResult.IsValid)
                {
                    invocation.ReturnValue = Result.Failure<UserDto>(validationResult.Errors.Select(x => x.ErrorMessage).First());
                    return;
                }
            }


            if (invocation.Arguments[0] is WordDto argWord)
            {
                if (argWord == null) { invocation.Proceed(); return; }

                var validator = _kernel.Get<IValidator<WordDto>>();
                var validationResult = validator.Validate(instance: argWord, ruleSet: "PreValidation"); // contract
                if (!validationResult.IsValid)
                {
                    invocation.ReturnValue = Result.Failure<WordDto>(validationResult.Errors.Select(x => x.ErrorMessage).First());
                    return;
                }
            }

           
            
            invocation.Proceed();
        }
    }
}
