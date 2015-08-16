namespace Adform.SummerCamp.TowerDefense.Console.Objects
{
    public class AttackerUpgrader
    {
        public float SpeedMultiplier { set; get; }
        public float ArmorMultiplier { set; get; }

        public AttackerUpgrader()
        {
            this.SpeedMultiplier = 1;
            this.ArmorMultiplier = 1;
        }

        public void UpgradeSpeed()
        {
            this.SpeedMultiplier += (float)0.1;
        }
        public void UpgradeArmor()
        {
            this.ArmorMultiplier *= (float)0.9;
        }
    }
}
