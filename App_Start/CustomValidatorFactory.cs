using FluentValidation;
using Ninject;
using System;
using System.Web.Http.Dependencies;

namespace GamePortal.Web.Api
{
    public class CustomValidatorFactory : IValidatorFactory
    {
        private IDependencyResolver _dependencyResolver;
        private IKernel _kernel;

        public CustomValidatorFactory(IDependencyResolver dependencyResolver)
        {
            this._dependencyResolver = dependencyResolver;
        }

        public CustomValidatorFactory(IKernel kernel)
        {
            this._kernel = kernel;
        }

        public IValidator<T> GetValidator<T>()
        {
           return _kernel.GetService(typeof(T)) as IValidator<T>;
        }

        public IValidator GetValidator(Type type)
        {
            return _kernel.GetService(type) as IValidator;
        }

    }
}
