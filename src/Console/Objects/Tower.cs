using System;

namespace Adform.SummerCamp.TowerDefense.Console.Objects
{
    public class Tower
    {
        public uint Range;
        public uint AttackSpeed;
        public uint Damage;
        public Guid CellId;

        public Tower(uint range, uint attackSpeed, uint damage, Guid cellId)
        {
            this.Range = range;
            this.AttackSpeed = attackSpeed;
            this.Damage = damage;
            this.CellId = cellId;
        }
    }
}
