using System.Threading.Tasks;
using Adform.SummerCamp.TowerDefense.Console.Hubs;
using Adform.SummerCamp.TowerDefense.Console.States;
using Adform.SummerCamp.TowerDefense.Console.Objects;

namespace Adform.SummerCamp.TowerDefense.Console.Controllers
{
    public class RoundController
    {
        private static RoundState RoundState;
        public int roundCount = 1;

        public void StartGameLoop(IApiClient client)
        {
            RoundState = new RoundState();
            RoundState.IsRoundStarted = true;

            client.RoundStarded();

            //bool success = true;
            Task.Factory.StartNew(() =>
            {
                TowerStartedShooting(client);
                for (int i = 0; i < 10; i++)
                {
                    AttackerMove(client, 0, 0);
                    AttackerRecievedDamage(client);
                    //TowerStopedShooting();
                    // do something
                    Task.Delay(100).Wait();
                }
                roundCount++;
                RoundState.IsRoundStarted = false;
            });
        }

        private void AttackerMove(IApiClient client, int posX, int posY)
        {
            System.Console.Out.WriteLine("MOVING >:D");
            client.AttackerMoved(posX, posY);
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
                System.Console.Out.WriteLine("You loose bastard");
                EndOfRound(client ,true);
            }
            else
            {
                System.Console.Out.WriteLine("-10 hp!!! HEALTH LEFT:" + RoundState.AttackerInfo.CurrentHealth);
            }
        }

        //End of round
        private void EndOfRound(IApiClient client, bool defenderWon, SetupState setupState)
        {
            client.RoundFinished();
            if (defenderWon)
            {
                if (roundCount >= 5)
                {
                    setupState.IsAttackerReady = false;
                    setupState.IsDefenderReady = false;
                }
                else
                {
                    client.DefenderWon();
                    System.Console.Out.WriteLine("Round END");
                }
            }
            else
            {
                client.AttackerWon();
            }
        }
    }
}