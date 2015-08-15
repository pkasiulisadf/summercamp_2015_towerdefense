namespace Adform.SummerCamp.TowerDefense.Console.States
{
    class SetupState
    {
        private bool IsDefenderready { set; get; }
        private bool IsAtackerReady { set; get; }

        public SetupState()
        {
            this.IsDefenderready = false;
            this.IsAtackerReady = false;
        }
    }
}
