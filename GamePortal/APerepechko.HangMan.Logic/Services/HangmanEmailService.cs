using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace APerepechko.HangMan.Logic.Services
{
    internal class HangmanEmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            //здесь конкретная реализация smtp client


            return Task.CompletedTask;
        }
    }
}