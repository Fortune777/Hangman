using APerepechko.HangMan.Logic.Model;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GamePortal.Web.Api.Hubs
{
    public interface IHangmanClient
    {
        //Task WordAdded(WordDto model);
        Task UpdateMessage(string msg);
    }

    [HubName("sample")]
    public class HangmanHub : Hub<IHangmanClient>
    {
        public async Task UpdateMessage(string msg)
        {
            //Clients.All.gotMessage(msg); -- 
           await  Clients.All.UpdateMessage(msg);
        }
    }
}