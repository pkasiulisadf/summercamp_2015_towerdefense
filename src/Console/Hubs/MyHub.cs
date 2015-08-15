using System.Threading.Tasks;
using Adform.SummerCamp.TowerDefense.Console.States;
using Microsoft.AspNet.SignalR;

namespace Adform.SummerCamp.TowerDefense.Console.Hubs
{
    public class MyHub : Hub
    {

        private static GameRoomState GameRoomState;
        private static SetupState SetupState = new SetupState();
        private static RoundState RoundState = new RoundState();
 
        public void Send(string name, string message)
        {
            System.Console.Out.WriteLine(message);
        }

        public void CreateGameRoom()
        {
            GameRoomState = new GameRoomState();
            Clients.All.GameRoomCreated();
        }

        public void ConnectAttacker()
        {
            Clients.All.AttackerConnected();
            GameRoomState.IsAttackerConnected = true;
        }

        public void MarkAttackerReady()
        {
            Clients.All.AttackerWasMarkedReady();
            if (GameRoomState.IsAttackerConnected && GameRoomState.IsDefenderConnected)
            {
                SetupStarted();
            }
        }

        public void ConnectDefender()

        {
            Clients.All.DefenderConnected();
            GameRoomState.IsDefenderConnected = true;
            if (GameRoomState.IsAttackerConnected && GameRoomState.IsDefenderConnected)
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
            SetupState.IsAttackerReady = true;
            OnPlayerReady();
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
                Clients.All.roundStarded();
                Update();
            }
        }

        public void AttackerMoved(int PosX,int PosY)
        {
            Clients.All.AttackerMoved(PosX, PosY);
        }

        public void EndOfRound(bool defenderWon)
        {
            Clients.All.RoundFinished();
            if(defenderWon)
                Clients.All.DefenderWon();
            else
                Clients.All.AttackerWon();
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