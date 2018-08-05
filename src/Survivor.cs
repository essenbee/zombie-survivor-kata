using System;
using System.Collections.Generic;

namespace ZombieSurvivor.Core
{
    public class Survivor
    {
        public string Name { get; }
        public int Wounds { get; private set; } = 0;
        public bool IsAlive => Wounds < MaxWounds;
        public int ActionsRemaining { get; set; } = ActionsPerTurn;
        public int CarryingCapacity
        {
            get
            {
                return MaxCarryingCapacity - Wounds;
            }
        }
        public IList<Equipment> EquipmentCarried { get; set; }

        public const int ActionsPerTurn = 3;
        public const int MaxWounds = 2;
        public int MaxCarryingCapacity = 5;

        public Survivor(string name)
        {
            Name = name;
            EquipmentCarried = new List<Equipment>();
        }

        public void SustainInjury(int numberOfWounds)
        {
            Wounds = Wounds + numberOfWounds <= MaxWounds
                ? Wounds + numberOfWounds
                : MaxWounds;

            if (EquipmentCarried.Count > CarryingCapacity)
            {
                // ToDo: Drop a piece of Equipment
            }
        }

        public bool PickUpEquipment(Equipment equipment)
        {
            if (EquipmentCarried.Count < CarryingCapacity)
            {
                EquipmentCarried.Add(equipment);
                return true;
            }

            return false;
        }
    }
}
