using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace APerepechko.HangMan.Logic.Identity
{
    public interface IUserService
    {
        Task<Result> Register(NewUserDto model);
    }

    public class NewUserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> userManager)
        {
            this._userManager = userManager;
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
            await _userManager.ResetPasswordAsync(userId,token,"2233");

        }


    }
}
