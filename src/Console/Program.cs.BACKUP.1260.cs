﻿using System;
<<<<<<< HEAD
using System.Threading.Tasks;
=======
using Console;
>>>>>>> a35a528e65eb58f05d8775cdc8c86f4d7397b535
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
        private static GameRoomState gameRoomState = new GameRoomState();

        public void send(string name, string message)
        {
            Console.Out.WriteLine(message);
        }
        public void createGameRoom()
        {
            Clients.All.gameRoomCreated();
            gameRoomState.IsAttackerCreated = false;
            gameRoomState.IsDefenderCreated = false;
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
            update();
        }

        public void update()
        {
            bool success = true;
            Task.Factory.StartNew(() =>
            {
                for(int i=0;i<10;i++)
                {
                    Console.Out.WriteLine("move");
                    // do something
                    Task.Delay(100).Wait();
                }
            });
        }
    }

}