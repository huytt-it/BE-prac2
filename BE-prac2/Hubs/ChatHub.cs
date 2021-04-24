using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BE_prac2.ViewModels;
using Data.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace BE_prac2.Hubs
{

    public interface IChatClient
    {
        Task ReceiveMessage(MessageModel message);
        Task GetUserOnline(string idConnect);
        Task GetUserDisconnect(String idConnect);


    }
    public class ChatHub:Hub<IChatClient>
    {
        public override Task OnConnectedAsync()
        {

            UserHandler.ConnectedIds.Add(new ConnectedUser{IdConnected = Context.ConnectionId});
            Clients.All.GetUserOnline(Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var thisId =UserHandler.ConnectedIds.FirstOrDefault(x => x.IdConnected.Equals(Context.ConnectionId));
            UserHandler.ConnectedIds.Remove(thisId);
            Clients.All.GetUserDisconnect(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
