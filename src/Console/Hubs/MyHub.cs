using System.Linq.Expressions;
using System.Threading.Tasks;
using Adform.SummerCamp.TowerDefense.Console.States;
using Microsoft.AspNet.SignalR;

namespace Adform.SummerCamp.TowerDefense.Console.Hubs
{
    public class MyHub : Hub
    {

        private static GameRoomState gameRoomState;
        private static SetupState setupState = new SetupState();
        private static RoundState roundState = new RoundState();
 
        public void Send(string name, string message)
        {
            System.Console.Out.WriteLine(message);
        }

        public void CreateGameRoom()
        {
            gameRoomState = new GameRoomState();
            Clients.All.gameRoomCreated();
        }

        public void CreateAttacker()
        {
            Clients.All.attackerCreated();
            gameRoomState.IsAttackerConnected = true;
            if (gameRoomState.IsAttackerConnected && gameRoomState.IsDefenderConnected)
            {
                setupStarted();
            }
        }

        public void CreateDefender()

        {
            Clients.All.defenderCreated();
            gameRoomState.IsDefenderConnected = true;
            if (gameRoomState.IsAttackerConnected && gameRoomState.IsDefenderConnected)
            {
                setupStarted();
            }
        }

        public void setupStarted()
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
                Clients.All.roundStarded();
                Update();
            }
        }

        public void EndOfRound(bool defenderWon)
        {
            Clients.All.roundFinished();
            if(defenderWon)
                Clients.All.defenderWon();
            else
                Clients.All.attackerWon();
        }

        public void Update()
        {
            //bool success = true;
            Task.Factory.StartNew(() =>
            {
                for(int i=0;i<10;i++)
                {
                    System.Console.Out.WriteLine("move");
                    // do something
                    Task.Delay(100).Wait();
                }
            });
        }
    }
}