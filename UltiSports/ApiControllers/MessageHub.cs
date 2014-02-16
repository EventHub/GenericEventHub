using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace UltiSports.ApiControllers
{
    public class MessageHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void NewChatMessage(string eventName, string user, string message)
        {
            Clients.All.sendMessage(eventName, user, message);
        }
    }
}