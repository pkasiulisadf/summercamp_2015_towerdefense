﻿using System;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;

namespace SignalRSelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            // This will *ONLY* bind to localhost, if you want to bind to all addresses
            // use http://*:8080 to bind to all addresses. 
            // See http://msdn.microsoft.com/en-us/library/system.net.httplistener.aspx 
            // for more information.
            string url = "http://localhost:43210";
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Server running on {0}", url);
                Console.ReadLine();
            }
        }
    }
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
    public class MyHub : Hub
    {
        public void send(string name, string message)
        {
            Console.Out.WriteLine(message);
        }
        public void createGameRoom()
        {
            Clients.All.gameRoomCreated();
        }
        public void createAttacker()
        {
            Clients.All.attackerCreated();
        }
        public void attackerReady()
        {
            Clients.All.attackerPrepared();
        }
        public void createDefender()
        {
            Clients.All.defenderCreated();
        }
        public void defenderReady()
        {
            Clients.All.defenderPrepared();
            Clients.All.roundStarded();
        }
    }
}