﻿using System;
using Adform.SummerCamp.TowerDefense.Console.Hubs;
using Adform.SummerCamp.TowerDefense.Console.Objects;
using Adform.SummerCamp.TowerDefense.Console.States;

namespace Adform.SummerCamp.TowerDefense.Console.Controllers
{
    public class SetupController
    {
        private static SetupState SetupState;

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
                roundController.StartGameLoop(client, SetupState, this);
            }
        }

        public void BeginFirstRoundSetup(IApiClient client)
        {
            SetupState = new SetupState();
            Map defMap = new Map();
            SetupState.Map = defMap.defaultMap();
            client.SetupStarted(SetupState.Map);
        }

        public void BeginNextRoundSetup(IApiClient client, SetupState setupState)
        {
            SetupState = setupState;
            client.SetupStarted(SetupState.Map);
        }

        public void PlaceTower(IApiClient client ,Guid cellId)
        {
            Tower TempTower = new Tower(1,1,1,cellId);
            SetupState.Towers.Add(TempTower);
            client.TowerCreated(cellId);
        }
    }
}
