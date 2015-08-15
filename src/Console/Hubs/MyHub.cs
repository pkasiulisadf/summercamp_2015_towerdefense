using System.Threading.Tasks;
using Adform.SummerCamp.TowerDefense.Console.Controllers;
using Adform.SummerCamp.TowerDefense.Console.Objects;
using Adform.SummerCamp.TowerDefense.Console.States;
using Microsoft.AspNet.SignalR;

namespace Adform.SummerCamp.TowerDefense.Console.Hubs
{
    public class MyHub : Hub<IApiClient>
    {

        private static SetupState SetupState = new SetupState();
        private static RoundState RoundState = new RoundState();

        private GameRoomController gameRoomController;

        public MyHub()
        {
            this.gameRoomController = new GameRoomController();
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
                if (!RoundState.IsRoundStarted)
                {
                    Clients.All.RoundStarded();
                    RoundState.IsRoundStarted = true;
                    Update();
                }
            }
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