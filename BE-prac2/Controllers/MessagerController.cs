using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.SignalR;
using Services.Services;

namespace BE_prac2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagerController : ControllerBase
    {
        private IHubContext<SignalRMessages, IMessages> _messageHub;

        public MessagerController(IHubContext<SignalRMessages, IMessages> messageHub)
        {
            _messageHub = messageHub;
        }

        [HttpPost("notify")]
        public async Task Post(Notification message)
        {
            await _messageHub.Clients.All.ReceiveMessage(message);
        }

    }
}
