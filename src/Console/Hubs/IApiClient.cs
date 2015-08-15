namespace Adform.SummerCamp.TowerDefense.Console.Hubs
{
    public interface IApiClient
    {
        void GameRoomCreated();
        void AttackerWasMarkedReady();
    }
}