using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Services.Services
{
    public interface IMessages
    {
        Task ReceiveMessage(Object message);
    }

    public class SignalRMessages:Hub<IMessages>
    {
    }
}
