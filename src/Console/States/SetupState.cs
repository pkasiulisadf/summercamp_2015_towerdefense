using Adform.SummerCamp.TowerDefense.Console.Objects;
using System.Collections.Generic;

namespace Adform.SummerCamp.TowerDefense.Console.States
{
    public class SetupState
    {
        public bool IsAttackerReady { get; set; }
        public bool IsDefenderReady { get; set; }
        public Map Map { get; set; }
        public List<Tower> Towers = new List<Tower>();
    }
}
