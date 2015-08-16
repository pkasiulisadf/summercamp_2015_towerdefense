using System;
using System.Linq;
using System.Threading.Tasks;
using Adform.SummerCamp.TowerDefense.Console.Hubs;
using Adform.SummerCamp.TowerDefense.Console.Objects;
using Adform.SummerCamp.TowerDefense.Console.States;
using Microsoft.CSharp;

namespace Adform.SummerCamp.TowerDefense.Console.Controllers
{
    public class RoundController
    {
        private static RoundState RoundState;
        public int roundCount = 1;

        public void StartGameLoop(IApiClient client, SetupState setupState, SetupController setupController)
        {
            if (RoundState != null && RoundState.IsRoundStarted)
            {
                System.Console.WriteLine("Game is already started");
                client.ErrorOccured("Game is already started");
                return;
            }

            RoundState = new RoundState();
            RoundState.IsRoundStarted = true;
            
            client.RoundStarded();

            //bool success = true;
            Task.Factory.StartNew(() =>
            {
                InitializeAttackerInfo(setupState);
                while (!IsRoundOver(setupState))
                {
                    IsAttackerInRange(client, setupState);
                    AttackerMove(client);
                    AttackerRecievedDamage(client);
                    
                    Task.Delay(300).Wait();
                }

                StopShooting(client);
                RoundState.IsRoundStarted = false;

                if (setupState.RoundNo < 5)
                {
                    client.RoundFinished();
                    setupController.BeginNextRoundSetup(client, setupState);
                    setupState.RoundNo++;
                }
                else
                {
                    EndOfGame(client);
                }
            });
        }

        private void StopShooting(IApiClient client)
        {
            foreach (var shootingTower in RoundState.ShootingTowers)
            {
                TowerIsIdle(client, shootingTower);
            }
        }

        private void IsAttackerInRange(IApiClient client, SetupState setupState)
        {
            foreach(Tower tower in setupState.Towers)
            {
                Cell towerCell = setupState.Map.Cells.First(cell => cell.CellId == tower.CellId);
                AttackerInfo attackerInfo = RoundState.AttackerInfo;
                float x = Math.Abs(towerCell.PosX - attackerInfo.PositionX);
                float y = Math.Abs(towerCell.PosY - attackerInfo.PositionY);
                double diagonal = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                if (tower.Range >= diagonal)
                {
                    TowerIsShooting(client, tower.CellId);
                    //AttackerRecievedDamage(client);
                }
                else
                {
                    TowerIsIdle(client, tower.CellId);
                }
            }
        }

        private void TowerIsIdle(IApiClient client, Guid towerId)
        {
            if (RoundState.ShootingTowers.Contains(towerId))
            {
                RoundState.ShootingTowers.Remove(towerId);
                client.TowerStoppedShooting(towerId);
                System.Console.WriteLine("Tower stopped shooting");
            }
        }

        private void TowerIsShooting(IApiClient client, Guid towerId)
        {
            if (!RoundState.ShootingTowers.Contains(towerId))
            {
                RoundState.ShootingTowers.Add(towerId);
                client.TowerStartedShooting(towerId);
                System.Console.WriteLine("Tower started shooting");
            }
        }

        private void InitializeAttackerInfo(SetupState setupState)
        {
            Cell startCell = setupState.Map.Cells.First(cell => cell.Type == "Start");

            RoundState.AttackerInfo = new AttackerInfo();
            RoundState.AttackerInfo.CurrentHealth = 100;
            RoundState.AttackerInfo.MaxHealth = 100;
            RoundState.AttackerInfo.PositionX = startCell.PosX;
            RoundState.AttackerInfo.PositionY = startCell.PosY;
        }

        private bool IsRoundOver(SetupState setupState)
        {
            Cell finishCell = setupState.Map.Cells.First(cell => cell.Type == "Finish");
            bool hasAttackerReachedFinish = RoundState.AttackerInfo.PositionX >= finishCell.PosX;
            bool isAttackerDead = RoundState.AttackerInfo.CurrentHealth <= 0;
            return hasAttackerReachedFinish || isAttackerDead;
        }

        private void AttackerMove(IApiClient client)
        {
            RoundState.AttackerInfo.PositionX += 1;

            System.Console.Out.WriteLine("MOVING >:D (x:{0} y:{1})", RoundState.AttackerInfo.PositionX, RoundState.AttackerInfo.PositionY);
            client.AttackerMoved((int)RoundState.AttackerInfo.PositionX, (int)RoundState.AttackerInfo.PositionY);
        }

        //Damage recieved by tower
        private void AttackerRecievedDamage(IApiClient client)
        {
            RoundState.AttackerInfo.CurrentHealth -= 1;
            if (RoundState.AttackerInfo.CurrentHealth <= 0)
            {
                System.Console.Out.WriteLine("Attacker lost!");
            }
            else
            {
                client.AttackerReceivedDamage(RoundState.AttackerInfo.CurrentHealth);
                System.Console.Out.WriteLine("-1 hp!!! HEALTH LEFT:" + RoundState.AttackerInfo.CurrentHealth);
            }
        }

        //End of round
        private void EndOfGame(IApiClient client)
        {
            if (RoundState.AttackerInfo.CurrentHealth <= 0)
            {
                client.DefenderWon();
                System.Console.Out.WriteLine("Round END");
            }
            else
            {
                client.AttackerWon();
                System.Console.Out.WriteLine("Attacker won!");
                System.Console.Out.WriteLine("Round END");
            }
        }
    }
}