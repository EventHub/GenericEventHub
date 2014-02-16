using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using UltiSports.Models;
using Microsoft.AspNet.SignalR.Hubs;

namespace UltiSports.ApiControllers
{
    public class MessageHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void NewChatMessage(Message message)
        {
            Clients.All.sendMessage(message);
        }
    }
}