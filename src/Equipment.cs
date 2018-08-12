namespace ZombieSurvivor.Core
{
    public class Equipment
    {
        public string Name { get; }
        public bool InHand { get; set; }
        public bool CanKillZomies { get; }
        public bool CanOpenDoors { get; }
        public bool IsNoisy { get; }
        public int Range { get; }
        public int DicePerAction { get; }
        public int Accuracy { get; }
        public int Damage { get; }

        public Equipment(string name, bool killsZombies = false, bool opensDoors = false, bool noisy = false,
            int range = 0, int dice = 0, int accuracy = 0, int damage = 0)
        {
            Name = name;
            CanKillZomies = killsZombies;
            CanOpenDoors = opensDoors;
            IsNoisy = noisy;
            Range = range;
            DicePerAction = dice;
            Accuracy = accuracy;
            Damage = damage;
        }
    }
}
