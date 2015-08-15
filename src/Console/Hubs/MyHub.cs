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
 
        public void send(string name, string message)
        {
            System.Console.Out.WriteLine(message);
        }

        public void createGameRoom()
        {
            Clients.All.gameRoomCreated();
            gameRoomState.IsAttackerConnected = false;
            gameRoomState.IsDefenderConnected = false;
        }

        public void createAttacker()
        {
            Clients.All.attackerCreated();
            gameRoomState.IsAttackerConnected = true;
        }

        public void attackerReady()
        {
            Clients.All.attackerPrepared();
        }

        public void createDefender()
        {
            Clients.All.defenderCreated();
            gameRoomState.IsDefenderConnected = true;
        }

        public void defenderReady()
        {
            Clients.All.defenderPrepared();
            Clients.All.roundStarded();
            update();
        }
        
        public void endOfRound(bool defenderWon)
        {
            Clients.All.roundFinished();
            if(defenderWon)
                Clients.All.defenderWon();
            else
                Clients.All.attackerWon();
        }

        public void update()
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