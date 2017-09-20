using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using WebApp.Models;

namespace WebApp.Infrastructure
{
    public class ContactHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void GenerateNotification(CustomMessages msg)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(msg.Message);
        }
    }
}