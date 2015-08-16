﻿using System;
using Adform.SummerCamp.TowerDefense.Console.Hubs;
using Adform.SummerCamp.TowerDefense.Console.Objects;
using Adform.SummerCamp.TowerDefense.Console.States;

namespace Adform.SummerCamp.TowerDefense.Console.Controllers
{
    public class SetupController
    {
        private static SetupState SetupState = new SetupState();

        public void MarkAttackerReady(IApiClient client, RoundController roundController)
        {
            if (GameRoomController.GameRoomState == null || !GameRoomController.GameRoomState.IsBothPlayersConnected())
            {
                client.ErrorOccured("Trying mark attacker as ready before room created and/or both players are ready.");
                return;
            }
            client.AttackerWasMarkedReady();
            SetupState.IsAttackerReady = true;
            OnPlayerReady(client, roundController);
        }

        public void MarkDefenderReady(IApiClient client, RoundController roundController)
        {
            if (GameRoomController.GameRoomState == null || !GameRoomController.GameRoomState.IsBothPlayersConnected())
            {
                client.ErrorOccured("Trying mark defender as ready before room created and/or both players are ready.");
                return;
            }
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
        }

        public void PlaceTower(IApiClient client ,Guid cellId)
        {
            Tower TempTower = new Tower(10,4,1,cellId,"Default range");
            SetupState.Towers.Add(TempTower);
            client.TowerCreated(cellId);
        }
        public void PlaceLongRageTower(IApiClient client, Guid cellId)
        {
            Tower LongTower = new Tower(20, 2, 2, cellId, "Long range");
            SetupState.Towers.Add(LongTower);
            client.TowerCreated(cellId);
        }
        public void PlaceShortRageTower(IApiClient client, Guid cellId)
        {
            Tower ShortTower = new Tower(5, 6, 1, cellId, "Short range");
            SetupState.Towers.Add(ShortTower);
            client.TowerCreated(cellId);
        }
    }
}
