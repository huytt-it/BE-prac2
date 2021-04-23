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
        Task OnConnectedAsync();


    }
    public class ChatHub:Hub<IChatClient>
    {
        public List<UserHandler> _connectedId = new List<UserHandler>();
        public override Task OnConnectedAsync()
        {

            UserHandler.ConnectedIds.Add(new ConnectedUser{IdConnected = Context.ConnectionId});
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var thisId =UserHandler.ConnectedIds.FirstOrDefault(x => x.IdConnected.Equals(Context.ConnectionId));
            UserHandler.ConnectedIds.Remove(thisId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
