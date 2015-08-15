using Adform.SummerCamp.TowerDefense.Console.Controllers;
using Adform.SummerCamp.TowerDefense.Console.Objects;
using Adform.SummerCamp.TowerDefense.Console.States;
using Microsoft.AspNet.SignalR;

namespace Adform.SummerCamp.TowerDefense.Console.Hubs
{
    public class MyHub : Hub<IApiClient>
    {

        private static SetupState SetupState = new SetupState();

        private GameRoomController gameRoomController;
        private RoundController roundController;

        public MyHub()
        {
            this.gameRoomController = new GameRoomController();
            this.roundController = new RoundController();
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
            Clients.All.AttackerWasMarkedReady();
            SetupState.IsAttackerReady = true;
            OnPlayerReady();
        }

        public void ConnectDefender()
        {
        }

        public void SetupStarted()
        {
            Map defMap = new Map();
            Clients.All.SetupStarted(defMap);
            Clients.All.TowerCreated();
        }

        public void MarkDefenderReady()
        {
            Clients.All.DefenderWasMarkedReady();
            SetupState.IsDefenderReady = true;
            OnPlayerReady();

        }

        private void OnPlayerReady()
        {
            if (SetupState.IsDefenderReady && SetupState.IsAttackerReady)
            {
                roundController.StartGameLoop();
            }
        }
    }
}