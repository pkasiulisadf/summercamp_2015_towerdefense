using System.Collections.Generic;

namespace Adform.SummerCamp.TowerDefense.Console.Objects
{
    public class Map
    {
        public List<Cell> Cells = new List<Cell>();
        public uint PosX { set; get; }
        public uint PosY { set; get; }
        public List<Cell> GenerateMap (string[] mapStrings)
        {
            uint cellSize = 9;
            uint cellOffset = 5;
                uint j =0;
            foreach (string mapString in mapStrings)
            {
                PosY=cellSize*(uint)mapString.Length;
                PosX = cellSize * (uint)mapStrings.Length;
                for (int i = 0; i < mapString.Length; i++)
                {
                    switch (mapString[i])
                    {
                        case 'E': 
                            Cells.Add(new Cell(cellSize * (uint)i + cellOffset, cellSize * (uint)j + cellOffset, "Empty"));
                            break;
                        case 'P': 
                            Cells.Add(new Cell(cellSize * (uint)i + cellOffset, cellSize * (uint)j + cellOffset, "Placement"));
                            break;
                        case 'R': 
                            Cells.Add(new Cell(cellSize * (uint)i + cellOffset, cellSize * (uint)j + cellOffset, "Road"));
                            break;
                        case 'S': 
                            Cells.Add(new Cell(cellSize * (uint)i + cellOffset, cellSize * (uint)j + cellOffset, "Start"));
                            break;
                        case 'F': 
                            Cells.Add(new Cell(cellSize * (uint)i + cellOffset, cellSize * (uint)j + cellOffset, "Finish"));
                            break;
                        default:
                            Cells.Add(new Cell(cellSize * (uint)i + cellOffset, cellSize * (uint)j + cellOffset, "Empty"));
                            break;                           
                    }
                }
                j++;
            }
                return Cells;
        }
        public Map defaultMap()
        {
            Map defMap = new Map();
            string[] mapStrings = new string[3];
            mapStrings[0]="EPEEEPEEEPE";
            mapStrings[1]="SRRRRRRRRRF";
            mapStrings[2]="EPEPEEEPEPE";
            defMap.Cells = GenerateMap(mapStrings);
            defMap.PosX=this.PosX;
            defMap.PosY=this.PosY;

            return defMap;
        }
    }
}
