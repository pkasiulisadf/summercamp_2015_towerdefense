using System;

namespace Adform.SummerCamp.TowerDefense.Console.Objects
{
    public class Cell
    {
        public Guid CellId { set; get; }
        public uint PosX { set; get; }
        public uint PosY { set; get; }
        public string Type { set; get; }

        public Cell(uint PosX, uint PosY, string Type)
        {
            this.PosX = PosX;
            this.PosY = PosY;
            this.Type = Type;
            this.CellId = Guid.NewGuid();
        }
    }
}
