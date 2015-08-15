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
 
        public void send(string name, string message)
        {
            System.Console.Out.WriteLine(message);
        }

        public void createGameRoom()
        {
            gameRoomState = new GameRoomState();
            Clients.All.gameRoomCreated();
        }

        public void createAttacker()
        {
            Clients.All.attackerCreated();
            gameRoomState.IsAttackerConnected = true;
        }

        public void AttackerReady()
        {
            Clients.All.attackerPrepared();
        }

        public void CreateDefender()
        {
            Clients.All.defenderCreated();
            gameRoomState.IsDefenderConnected = true;
        }

        public void DefenderReady()
        {
            Clients.All.defenderPrepared();
            Clients.All.roundStarded();
            Update();
        }
        
        public void AttackerMoved(int PosX,int PosY)
        {
            Clients.All.AttackerMoved(PosX, PosY);
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
            bool success = true;
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