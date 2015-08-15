using Adform.SummerCamp.TowerDefense.Console.Objects;
using System.Collections.Generic;

namespace Adform.SummerCamp.TowerDefense.Console.States
{
    class SetupState
    {
        public bool IsAttackerReady { get; set; }
        public bool IsDefenderReady { get; set; }

        public List<Tower> Towers = new List<Tower>();
    }
}
