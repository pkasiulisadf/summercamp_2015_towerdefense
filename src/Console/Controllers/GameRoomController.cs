using Adform.SummerCamp.TowerDefense.Console.Hubs;
using Adform.SummerCamp.TowerDefense.Console.States;

namespace Adform.SummerCamp.TowerDefense.Console.Controllers
{
    public class GameRoomController
    {
        public static GameRoomState GameRoomState;
        
        public void CreateGameRoom(IApiClient client, SetupController setupController)
        {
            GameRoomState = new GameRoomState();
            client.GameRoomCreated();
        }

        public void ConnectAttacker(IApiClient client, SetupController setupController)
        {
            if (!IsGameRoomCreated())
            {
                client.ErrorOccured("Trying connect attacker before room was created");
                return;
            }
            client.AttackerConnected();
            GameRoomState.IsAttackerConnected = true;
            if (GameRoomState.IsAttackerConnected && GameRoomState.IsDefenderConnected)
            {
                setupController.BeginFirstRoundSetup(client);
            }
        }

        public void ConnectDefender(IApiClient client, SetupController setupController)
        {
            if (!IsGameRoomCreated())
            {
                client.ErrorOccured("Trying connect defender before room was created");
                return;
            }
            client.DefenderConnected();
            GameRoomState.IsDefenderConnected = true;
            if (GameRoomState.IsAttackerConnected && GameRoomState.IsDefenderConnected)
            {
                setupController.BeginFirstRoundSetup(client);
            }
        }

        private bool IsGameRoomCreated()
        {
            if(GameRoomState == null)
            {
                return false;
            }
            return true;
        }
    }
}
