using System;
using Adform.SummerCamp.TowerDefense.Console.Objects;

namespace Adform.SummerCamp.TowerDefense.Console.Hubs
{
    public interface IApiClient
    {
        void GameRoomCreated();
        void AttackerWasMarkedReady();
        void AttackerConnected();
        void DefenderConnected();
        void SetupStarted();
        void TowerCreated(Guid cellId);
        void DefenderWasMarkedReady();
        void RoundStarded();
        void AttackerMoved(int posX, int posY);
        void RoundFinished();
        void DefenderWon();
        void AttackerWon();
        void ErrorOccured(string message);
        void AttackerReceivedDamage(int currentHealth);
        void TowerStoppedShooting(Guid towerId);
        void TowerStartedShooting(Guid towerId);
        void GameInitialized(Map map);
    }
}