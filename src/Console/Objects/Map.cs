﻿using System.Collections.Generic;

namespace Adform.SummerCamp.TowerDefense.Console.Objects
{
    class Map
    {
        public List<Cell> Cells = new List<Cell>();
        private uint PosX { set; get; }
        private uint PosY { set; get; }
        public List<Cell> GenerateMap (string[] mapStrings)
        {
            uint cellSize = 9;
            uint cellOffset = 5;
                uint j =0;
            foreach (string mapString in mapStrings)
            {
                PosX=cellSize*(uint)mapString.Length;
                PosY = cellSize * (uint)mapStrings.Length;
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
            mapStrings[0]="EPPPPPPPPPE";
            mapStrings[1]="SRRRRRRRRRF";
            mapStrings[2]="EPPPPPPPPPE";
            defMap.Cells = GenerateMap(mapStrings);
            defMap.PosX=PosX;
            defMap.PosY=PosY;

            return defMap;
        }
    }
}
