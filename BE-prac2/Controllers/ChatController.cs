using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BE_prac2.Hubs;
using BE_prac2.ViewModels;
using Data.ViewModels;
using Microsoft.AspNetCore.SignalR;

namespace BE_prac2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub, IChatClient> _chatHub;

        public ChatController(IHubContext<ChatHub, IChatClient> chatHub)
        {
            _chatHub = chatHub;
        }

        [HttpPost("messages")]
        public async Task Post(MessageModel message)
        {
            await _chatHub.Clients.All.ReceiveMessage(message);
        }

        [HttpPost("privateMessage")]
        public async Task PostPrivate(MessageModel message)
        {
            await _chatHub.Clients.User(message.Name).ReceiveMessage(message);
        }

        [HttpGet]
        public  IActionResult GetUserOnline()
        {
            var userOnline = UserHandler.ConnectedIds.ToList();
            return Ok(userOnline);
        }




    }
}
