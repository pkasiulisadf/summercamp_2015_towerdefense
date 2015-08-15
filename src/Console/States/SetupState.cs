using Adform.SummerCamp.TowerDefense.Console.Objects;

namespace Adform.SummerCamp.TowerDefense.Console.States
{
    public class SetupState
    {
        public bool IsAttackerReady { get; set; }
        public bool IsDefenderReady { get; set; }
        public Map Map { get; set; }
    }
}
