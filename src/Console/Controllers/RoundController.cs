using System.Linq;
using System.Threading.Tasks;
using Adform.SummerCamp.TowerDefense.Console.Hubs;
using Adform.SummerCamp.TowerDefense.Console.Objects;
using Adform.SummerCamp.TowerDefense.Console.States;
using Adform.SummerCamp.TowerDefense.Console.Objects;

namespace Adform.SummerCamp.TowerDefense.Console.Controllers
{
    public class RoundController
    {
        private static RoundState RoundState;

        public void StartGameLoop(IApiClient client, SetupState setupState)
        {
            RoundState = new RoundState();
            RoundState.IsRoundStarted = true;
            
            client.RoundStarded();

            //bool success = true;
            Task.Factory.StartNew(() =>
            {
                InitializeAttackerInfo(setupState);
                TowerStartedShooting(client);
                while (!IsRoundOver(setupState))
                {
                    AttackerMove(client);
                    AttackerRecievedDamage(client);
                    //TowerStopedShooting();
                    // do something
                    
                    Task.Delay(100).Wait();
                }
                EndOfRound(client);
                RoundState.IsRoundStarted = false;
            });
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

            System.Console.Out.WriteLine("MOVING >:D");
            client.AttackerMoved((int)RoundState.AttackerInfo.PositionX, (int)RoundState.AttackerInfo.PositionY);
        }

        private void TowerStartedShooting(IApiClient client)
        {
            System.Console.Out.WriteLine("Tower Shoots!");
        }

        private void TowerStopedShooting()
        {
            System.Console.Out.WriteLine("Tower Stops!");
        }

        //Damage recieved by tower
        private void AttackerRecievedDamage(IApiClient client)
        {
            RoundState.AttackerInfo.CurrentHealth -= 10;
            if (RoundState.AttackerInfo.CurrentHealth <= 0)
            {
                System.Console.Out.WriteLine("Attacker lost!");
            }
            else
            {
                System.Console.Out.WriteLine("-10 hp!!! HEALTH LEFT:" + RoundState.AttackerInfo.CurrentHealth);
            }
        }

        //End of round
        private void EndOfRound(IApiClient client)
        {
            client.RoundFinished();
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