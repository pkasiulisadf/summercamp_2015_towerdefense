using Adform.SummerCamp.TowerDefense.Console.Controllers;
using Microsoft.AspNet.SignalR;

namespace Adform.SummerCamp.TowerDefense.Console.Hubs
{
    public class MyHub : Hub<IApiClient>
    {
        private GameRoomController gameRoomController;
        private RoundController roundController;
        private SetupController setupController;

        public MyHub()
        {
            gameRoomController = new GameRoomController();
            roundController = new RoundController();
            setupController = new SetupController();
        }

        public void Send(string name, string message)
        {
            System.Console.Out.WriteLine(message);
        }

        public void CreateGameRoom()
        {
            gameRoomController.CreateGameRoom(Clients.All, setupController);
        }

        public void ConnectAttacker()
        {
            gameRoomController.ConnectAttacker(Clients.All, setupController);
        }

        public void ConnectDefender()
        {
            gameRoomController.ConnectDefender(Clients.All, setupController);
        }

        public void MarkAttackerReady()
        {
            setupController.MarkAttackerReady(Clients.All, roundController);
        }

        public void MarkDefenderReady()
        {
            setupController.MarkDefenderReady(Clients.All, roundController);
        }
    }
}