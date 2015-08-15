namespace Adform.SummerCamp.TowerDefense.Console.States
{
    public class GameRoomState
    {
        public bool IsAttackerConnected { get; set; }
        public bool IsDefenderConnected { get; set; }

        public GameRoomState()
        {
            this.IsAttackerConnected = false;
            this.IsDefenderConnected = false;
        }

        public bool IsBothPlayersConnected()
        {
            if (!IsAttackerConnected || !IsDefenderConnected)
                return false;

            return true;
        }
    }
}
