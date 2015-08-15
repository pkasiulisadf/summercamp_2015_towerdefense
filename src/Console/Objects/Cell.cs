using System;

namespace Adform.SummerCamp.TowerDefense.Console.Objects
{
    class Cell
    {
        private Guid CellId { set; get; }
        private uint Posx { set; get; }
        private uint PosY { set; get; }
        private string Type { set; get; }

        public Cell(uint PosX, uint PosY, string Type)
        {
            this.Posx = Posx;
            this.PosY = PosY;
            this.Type = Type;
            this.CellId = Guid.NewGuid();
        }
    }
}
