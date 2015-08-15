﻿namespace Adform.SummerCamp.TowerDefense.Console.States
{
    class GameRoomState
    {
        public bool IsAttackerConnected { get; set; }
        public bool IsDefenderConnected { get; set; }

        public GameRoomState()
        {
            this.IsAttackerConnected = false;
            this.IsDefenderConnected = false;
        }
    }
}
