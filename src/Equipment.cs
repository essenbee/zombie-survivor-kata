namespace ZombieSurvivor.Core
{
    public class Equipment
    {
        public string Name { get; }
        public bool InHand { get; set; }
        public bool CanKillZomies { get; set; }
        public bool CanOpenDoors { get; set; }
        public bool IsNoisy { get; set; }
        public int Range { get; set; }
        public int DicePerAction { get; set; }
        public int Accuracy { get; set; }
        public int Damage { get; set; }

        public Equipment(string name)
        {
            Name = name;
        }
    }
}
