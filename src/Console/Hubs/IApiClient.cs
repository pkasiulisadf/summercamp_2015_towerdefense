using Adform.SummerCamp.TowerDefense.Console.Objects;
using System;

namespace Adform.SummerCamp.TowerDefense.Console.Hubs
{
    public interface IApiClient
    {
        void GameRoomCreated();
        void AttackerWasMarkedReady();
        void AttackerConnected();
        void DefenderConnected();
        void SetupStarted(Map defMap);
        void TowerCreated(Guid cellId);
        void DefenderWasMarkedReady();
        void RoundStarded();
        void AttackerMoved(int posX, int posY);
        void RoundFinished();
        void DefenderWon();
        void AttackerWon();
    }
}