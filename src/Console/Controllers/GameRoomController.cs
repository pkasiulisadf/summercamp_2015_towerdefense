﻿using Adform.SummerCamp.TowerDefense.Console.Hubs;
using Adform.SummerCamp.TowerDefense.Console.Objects;
using Adform.SummerCamp.TowerDefense.Console.States;

namespace Adform.SummerCamp.TowerDefense.Console.Controllers
{
    public class GameRoomController
    {
        private static GameRoomState GameRoomState;
        
        public void CreateGameRoom(IApiClient client)
        {
            GameRoomState = new GameRoomState();
            client.GameRoomCreated();
        }

        public void ConnectAttacker(IApiClient client)
        {
            client.AttackerConnected();
            GameRoomState.IsAttackerConnected = true;
            if (GameRoomState.IsAttackerConnected && GameRoomState.IsDefenderConnected)
            {
                SetupStarted(client);
            }
        }

        public void ConnectDefender(IApiClient client)
        {
            client.DefenderConnected();
            GameRoomState.IsDefenderConnected = true;
            if (GameRoomState.IsAttackerConnected && GameRoomState.IsDefenderConnected)
            {
                SetupStarted(client);
            }
        }
        public void SetupStarted(IApiClient client)
        {
            Map defMap = new Map();
            client.SetupStarted(defMap.defaultMap());
            client.TowerCreated();
        }
    }
}
