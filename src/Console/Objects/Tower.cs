using System;

namespace Adform.SummerCamp.TowerDefense.Console.Objects
{
    public class Tower
    {
        public uint Range;
        public uint AttackSpeed;
        public uint Damage;
        public Guid CellId;
        public string Type;

        public Tower(uint range, uint attackSpeed, uint damage, Guid cellId,string type)
        {
            this.Range = range;
            this.AttackSpeed = attackSpeed;
            this.Damage = damage;
            this.CellId = cellId;
            this.Type = type;
        }
    }
}
