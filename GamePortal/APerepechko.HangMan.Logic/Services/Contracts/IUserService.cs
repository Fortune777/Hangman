using APerepechko.HangMan.Logic.Model;
using CSharpFunctionalExtensions;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APerepechko.HangMan.Logic.Services.Contracts
{
    public interface IUserService
    {
        Task<Result> Register(NewUserDto model);

        Task<Result> ConfirmEmail(string userId, string token);

        Task<Result> ResetPassword(string email);

        Task<Result> ChangePassword(string userId, string token, string newPassword);

        Task<Result<IReadOnlyCollection<UserDto>>> GetAllUsers();
        Task<Maybe<UserDto>> GetUser(string username, string password);

        Task<Result> RegisterExternalUser(ExternalLoginInfo externalLoginInfo);

    }
}
