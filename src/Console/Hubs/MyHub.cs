using System.Linq.Expressions;
using System.Threading.Tasks;
using Adform.SummerCamp.TowerDefense.Console.States;
using Microsoft.AspNet.SignalR;

namespace Adform.SummerCamp.TowerDefense.Console.Hubs
{
    public class MyHub : Hub
    {

        private static GameRoomState gameRoomState = new GameRoomState();
        private static SetupState setupState = new SetupState();
        private static RoundState roundState = new RoundState();
 
        public void Send(string name, string message)
        {
            System.Console.Out.WriteLine(message);
        }

        public void CreateGameRoom()
        {
            Clients.All.gameRoomCreated();
            gameRoomState.IsAttackerConnected = false;
            gameRoomState.IsDefenderConnected = false;
        }

        public void CreateAttacker()
        {
            Clients.All.attackerCreated();
            gameRoomState.IsAttackerConnected = true;
            if (gameRoomState.IsAttackerConnected && gameRoomState.IsDefenderConnected)
            {
                SetupStarted();
            }
        }

        public void CreateDefender()

        {
            Clients.All.defenderCreated();
            gameRoomState.IsDefenderConnected = true;
            if (gameRoomState.IsAttackerConnected && gameRoomState.IsDefenderConnected)
            {
                SetupStarted();
            }
        }

        public void SetupStarted()
        {

        }

        public void AttackerReady()
        {
            Clients.All.attackerPrepared();
            setupState.IsAttackerReady = true;
            OnPlayerReady();
        }

        public void DefenderReady()
        {
            Clients.All.defenderPrepared();
            setupState.IsDefenderReady = true;
            OnPlayerReady();

        }

        private void OnPlayerReady()
        {
            if (setupState.IsDefenderReady && setupState.IsAttackerReady)
            {
                if (!roundState.IsRoundStarted)
                {
                    Clients.All.roundStarded();
                    roundState.IsRoundStarted = true;
                    Update();
                }
            }
        }

        public void EndOfRound(bool defenderWon)
        {
            Clients.All.roundFinished();
            if (defenderWon)
            {
                Clients.All.defenderWon();
                System.Console.Out.WriteLine("GAME OVER ATTACKER!");
                setupState.IsAttackerReady = false;
                setupState.IsDefenderReady = false;
            }
            else
            {
                Clients.All.attackerWon();
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
                    roundState.IsRoundStarted = false;
                    EndOfRound(true);
                });
        }
    }
}