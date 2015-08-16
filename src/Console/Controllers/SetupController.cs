﻿using System;
using Adform.SummerCamp.TowerDefense.Console.Hubs;
using Adform.SummerCamp.TowerDefense.Console.Objects;
using Adform.SummerCamp.TowerDefense.Console.States;

namespace Adform.SummerCamp.TowerDefense.Console.Controllers
{
    public class SetupController
    {
        private static SetupState SetupState;
        private static AttackerUpgrader AttackerUpgrader = new AttackerUpgrader();

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
                SetupState.AttackerUpgrades.Add(AttackerUpgrader);
                roundController.StartGameLoop(client, SetupState, this);
            }
        }

        public void BeginFirstRoundSetup(IApiClient client)
        {
            SetupState = new SetupState();
            Map defMap = new Map();
            SetupState.Map = defMap.defaultMap();

            client.GameInitialized(SetupState.Map);
            client.SetupStarted();
        }

        public void BeginNextRoundSetup(IApiClient client, SetupState setupState)
        {
            SetupState = setupState;
            SetupState.IsAttackerReady = false;
            SetupState.IsDefenderReady = false;
            client.SetupStarted();
        }

        public void PlaceTower(IApiClient client ,Guid cellId)
        {
            Tower TempTower = new Tower(10,1,1,cellId);
            SetupState.Towers.Add(TempTower);
            client.TowerCreated(cellId);
        }

        public void UpgradeAttackerSpeed(IApiClient client, SetupState setupState)
        {
            AttackerUpgrader.UpgradeSpeed();
        }

        public void UpgradeAttackerArmor(IApiClient client, SetupState setupState)
        {
            AttackerUpgrader.UpgradeArmor();
        }
    }
}
