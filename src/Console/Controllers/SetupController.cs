using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adform.SummerCamp.TowerDefense.Console.Hubs;
using Adform.SummerCamp.TowerDefense.Console.Objects;
using Adform.SummerCamp.TowerDefense.Console.States;

namespace Adform.SummerCamp.TowerDefense.Console.Controllers
{
    class SetupController
    {
        public void MarkAttackerReady(IApiClient client)
        {
            client.AttackerWasMarkedReady();
            SetupState.IsAttackerReady = true;
            OnPlayerReady();
        }
    }
}
