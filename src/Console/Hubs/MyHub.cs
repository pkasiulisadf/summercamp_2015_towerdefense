using Adform.SummerCamp.TowerDefense.Console.Controllers;
using Adform.SummerCamp.TowerDefense.Console.Objects;
using Adform.SummerCamp.TowerDefense.Console.States;
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
            this.gameRoomController = new GameRoomController();
            this.roundController = new RoundController();
            this.setupController = new SetupController();
        }

        public void Send(string name, string message)
        {
            System.Console.Out.WriteLine(message);
        }

        public void CreateGameRoom()
        {
            gameRoomController.CreateGameRoom(Clients.All);
        }

        public void ConnectAttacker()
        {
            gameRoomController.ConnectAttacker(Clients.All);
        }

        public void MarkAttackerReady()
        {
            setupController.MarkAttackerReady(Clients.All);
        }

        public void ConnectDefender()
        {
        }


        public void MarkDefenderReady()
        {
            Clients.All.DefenderWasMarkedReady();
            SetupState.IsDefenderReady = true;
            OnPlayerReady();

        }

            }
        }
    }
}