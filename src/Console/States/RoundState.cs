﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adform.SummerCamp.TowerDefense.Console.States;
using Adform.SummerCamp.TowerDefense.Console.Objects;


namespace Adform.SummerCamp.TowerDefense.Console.States
{
    class RoundState
    {
        public static AttackerInfo AttackerInfo;
        //To do: kintamieji kurie bus round state viduje
        public bool IsRoundStarted { get; set; }
    }
}
