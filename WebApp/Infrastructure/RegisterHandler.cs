using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace WebApp.Infrastructure
{
    public static class SubscriberConfig
    {

        public static void Register()
        {
            IEventAggregator eventAggregator = DependencyResolver.Current.GetService<IEventAggregator>();
            Subscriber sub = new Subscriber(eventAggregator);
            sub.Subscribe<CustomMessages>(SubscriberActionMethod);
        }

        static void SubscriberActionMethod(CustomMessages msg)
        {
            var Clients = GlobalHost.ConnectionManager.GetHubContext<ContactHub>().Clients;
            Clients.All.broadcastMessage(msg.Message);

            //ContactHub hub = new ContactHub();
            //hub.GenerateNotification(msg);
        }


    }
}