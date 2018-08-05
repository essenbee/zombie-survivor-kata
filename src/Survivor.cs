using System;

namespace ZombieSurvivor.Core
{
    public class Survivor
    {
        public string Name { get; }
        public int Wounds { get; private set; } = 0;
        public bool IsAlive => Wounds < MaxWounds;

        public int ActionsRemaining { get; set; } = ActionsPerTurn;
        public const int ActionsPerTurn = 3;
        public const int MaxWounds = 2;

        public Survivor(string name)
        {
            Name = name;
        }

        public void SustainInjury(int numberOfWounds) => Wounds = Wounds + numberOfWounds <= MaxWounds
                ? Wounds + numberOfWounds
                : MaxWounds;
    }
}
