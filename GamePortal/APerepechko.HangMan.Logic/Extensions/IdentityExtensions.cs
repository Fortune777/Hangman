using CSharpFunctionalExtensions;
using Microsoft.AspNet.Identity;
using System.Linq;

namespace APerepechko.HangMan.Logic.Extensions
{
    public static class IdentityExtensions
    {
        public static Result ToFunctionalResult(this IdentityResult identityResult)
        {
            return identityResult.Succeeded ? Result.Success() : Result.Failure(identityResult.Errors.Aggregate((a, b) => $"{a},{b}"));
        }
    }
}
