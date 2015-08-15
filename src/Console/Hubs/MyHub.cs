using System.Threading.Tasks;
using Adform.SummerCamp.TowerDefense.Console.Controllers;
using Adform.SummerCamp.TowerDefense.Console.Objects;
using Adform.SummerCamp.TowerDefense.Console.States;
using Microsoft.AspNet.SignalR;

namespace Adform.SummerCamp.TowerDefense.Console.Hubs
{
    public class MyHub : Hub<IApiClient>
    {

        private static RoundState RoundState = new RoundState();

        private GameRoomController gameRoomController;
        private SetupController setupController;

        public MyHub()
        {
            this.gameRoomController = new GameRoomController();
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

        public void AttackerMoved(int PosX,int PosY)
        {
            Clients.All.AttackerMoved(PosX, PosY);
        }

        public void EndOfRound(bool defenderWon)
        {
            Clients.All.RoundFinished();
            if (defenderWon)
            {
                Clients.All.DefenderWon();
                System.Console.Out.WriteLine("GAME OVER ATTACKER!");
                SetupState.IsAttackerReady = false;
                SetupState.IsDefenderReady = false;
            }
            else
            {
                Clients.All.AttackerWon();
            }
        }

        public void TowerStartedShooting()
        {
            System.Console.Out.WriteLine("Tower Shoots!");
        }

        public void TowerStopedShooting()
        {
            System.Console.Out.WriteLine("Tower Stops!");
        }

        public void AttackerRecievedDamage()
        {
            System.Console.Out.WriteLine("-10 hp!!!");
        }

        public void AttackerMove()
        {
            System.Console.Out.WriteLine("MOVING >:D");
            AttackerMoved(0, 0);
        }


        public void Update()
        {
                //bool success = true;
                Task.Factory.StartNew(() =>
                {
                    for (int i = 0; i < 10; i++)
                    {
                        AttackerMove();
                        TowerStartedShooting();
                        AttackerRecievedDamage();
                        //TowerStopedShooting();
                        // do something
                        Task.Delay(100).Wait();
                    }
                    RoundState.IsRoundStarted = false;
                    EndOfRound(true);
                });
        }
    }
}