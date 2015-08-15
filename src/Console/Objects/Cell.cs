using System;

namespace Adform.SummerCamp.TowerDefense.Console.Objects
{
    class Cell
    {
        private Guid CellId { set; get; }
        private uint Posx { set; get; }
        private uint PosY { set; get; }

        public Cell(uint PosX, uint PosY)
        {
            this.Posx = Posx;
            this.PosY = PosY;
            this.CellId = Guid.NewGuid();
        }
    }
}
