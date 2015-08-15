using Adform.SummerCamp.TowerDefense.Console.Objects;

namespace Adform.SummerCamp.TowerDefense.Console.Hubs
{
    public interface IApiClient
    {
        void GameRoomCreated();
        void AttackerWasMarkedReady();
        void AttackerConnected();
        void DefenderConnected();
        void SetupStarted(Map defMap);
        void TowerCreated();
        void DefenderWasMarkedReady();
        void RoundStarded();
        void AttackerMoved(int posX, int posY);
        void RoundFinished();
        void DefenderWon();
        void AttackerWon();
    }
}