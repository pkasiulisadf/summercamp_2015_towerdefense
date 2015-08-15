using Adform.SummerCamp.TowerDefense.Console.Hubs;
using Adform.SummerCamp.TowerDefense.Console.Objects;
using Adform.SummerCamp.TowerDefense.Console.States;

namespace Adform.SummerCamp.TowerDefense.Console.Controllers
{
    public class GameRoomController
    {
        private static GameRoomState GameRoomState;
        
        public void CreateGameRoom(IApiClient client, SetupController setupController)
        {
            GameRoomState = new GameRoomState();
            client.GameRoomCreated();
        }

        public void ConnectAttacker(IApiClient client, SetupController setupController)
        {
            client.AttackerConnected();
            GameRoomState.IsAttackerConnected = true;
            if (GameRoomState.IsAttackerConnected && GameRoomState.IsDefenderConnected)
            {
                setupController.BeginFirstRoundSetup(client);
            }
        }

        public void ConnectDefender(IApiClient client, SetupController setupController)
        {
            client.DefenderConnected();
            GameRoomState.IsDefenderConnected = true;
            if (GameRoomState.IsAttackerConnected && GameRoomState.IsDefenderConnected)
            {
                setupController.BeginFirstRoundSetup(client);
            }
        }
    }
}
