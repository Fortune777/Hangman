using APerepechko.HangMan.Logic.Extensions;
using APerepechko.HangMan.Logic.Model;
using APerepechko.HangMan.Logic.Services.Contracts;
using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using Fody;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace APerepechko.HangMan.Logic.Identity
{

    [ConfigureAwait(false)]
    class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            this._mapper = mapper;
        }

        public async Task<Result> Register(NewUserDto model)
        {
            //create user 
            var user = new IdentityUser
            {
                Email = model.Email,
                UserName = model.UserName,
            };

            var result = await _userManager.CreateAsync(user, model.Password).ConfigureAwait(false);
            await _userManager.AddToRoleAsync(user.Id, "user");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);

            await _userManager.SendEmailAsync(user.Id, "Confirm your email",
                $"click on https://localhost:44313/api/user/email/confirm?userId={user.Id}&token={token}");


            return result.Succeeded ? Result.Success() : Result.Failure(result.Errors.Aggregate((a, b) => $"{a},{b}"));
        }


        public async Task ValidateEmailToken(string userId, string token)
        {
            var result = await _userManager.ConfirmEmailAsync(userId, token).ConfigureAwait(false);
            await _userManager.FindByEmailAsync("Boss@example.com").ConfigureAwait(false);
            await _userManager.GeneratePasswordResetTokenAsync(userId);
            await _userManager.ResetPasswordAsync(userId, token, "2233");
        }
        public async Task<Result> ConfirmEmail(string userId, string token)
        {
            var data = await _userManager.ConfirmEmailAsync(userId, token);
            return data.ToFunctionalResult();
        }

        public async Task<Result> ResetPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new ValidationException("User doesn't exist");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
            await _userManager.SendEmailAsync(user.Id, "Reset your password", $"Click on yourhost/api/users/password/reset?userId={user.Id}&token={token}");
            return Result.Success();
        }

        public async Task<Result<IReadOnlyCollection<UserDto>>> GetAllUsers()
        {
            //bad practice! don't use it in production. only as sample
            var users = await _userManager.Users.ProjectToListAsync<UserDto>(_mapper.ConfigurationProvider);
            return Result.Success((IReadOnlyCollection<UserDto>)users.AsReadOnly());
        }

        public Task<Result> ChangePassword(string userId, string token, string newPassword)
        { 
            throw new System.NotImplementedException();
        }
        public async Task<Maybe<UserDto>> GetUser(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return null;

            var isValid = await _userManager.CheckPasswordAsync(user, password);
            return isValid ? _mapper.Map<UserDto>(user) : null;
        }

        public async Task<Result> RegisterExternalUser(ExternalLoginInfo info)
        {
            var user = await _userManager.FindAsync(info.Login);
            if (user != null) return Result.Success();

            user = new IdentityUser(info.Email);
            await _userManager.CreateAsync(user);

            await _userManager.AddLoginAsync(user.Id, info.Login);
            return Result.Success();
        }
    }
}
