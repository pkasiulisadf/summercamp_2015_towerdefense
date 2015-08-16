using System;
using System.Collections.Generic;
using Adform.SummerCamp.TowerDefense.Console.Objects;

namespace Adform.SummerCamp.TowerDefense.Console.States
{
    class RoundState
    {
        public AttackerInfo AttackerInfo;
        //To do: kintamieji kurie bus round state viduje
        public bool IsRoundStarted { get; set; }
        public List<Guid> ShootingTowers { get; set; }

        public RoundState()
        {
            AttackerInfo = new AttackerInfo();


            ShootingTowers = new List<Guid>();

            IsRoundStarted = false;
        }
    }
}
