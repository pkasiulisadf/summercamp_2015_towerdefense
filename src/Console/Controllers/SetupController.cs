using Adform.SummerCamp.TowerDefense.Console.Hubs;
using Adform.SummerCamp.TowerDefense.Console.Objects;
using Adform.SummerCamp.TowerDefense.Console.States;
using Adform.SummerCamp.TowerDefense.Console.Objects;

namespace Adform.SummerCamp.TowerDefense.Console.Controllers
{
    public class SetupController
    {
        private static SetupState SetupState = new SetupState();
        public AttackerInfo AttackerInfo = new AttackerInfo();

        public void MarkAttackerReady(IApiClient client, RoundController roundController)
        {
            client.AttackerWasMarkedReady();
            SetupState.IsAttackerReady = true;
            OnPlayerReady(client, roundController);
        }

        public void MarkDefenderReady(IApiClient client, RoundController roundController)
        {
            client.DefenderWasMarkedReady();
            SetupState.IsDefenderReady = true;
            OnPlayerReady(client, roundController);
        }

        private void OnPlayerReady(IApiClient client, RoundController roundController)
        {
            if (SetupState.IsAttackerReady && SetupState.IsDefenderReady)
            {
                roundController.StartGameLoop(client, SetupState);
            }
        }

        public void BeginSetupState(IApiClient client)
        {
            Map defMap = new Map();
            SetupState.Map = defMap.defaultMap();
            client.SetupStarted(SetupState.Map);
            client.TowerCreated();
        }
    }
}
