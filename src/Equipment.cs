namespace ZombieSurvivor.Core
{
    public class Equipment
    {
        public string Name { get; }
        public bool InHand { get; set; }

        public Equipment(string name)
        {
            Name = name;
        }
    }
}
