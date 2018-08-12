using System;
using System.Collections.Generic;
using System.Linq;

namespace ZombieSurvivor.Core
{
    public class Survivor
    {
        public string Name { get; }
        public Level Level
        {
            get
            {
                if (Experience <= 6) return Level.Blue;
                if (Experience <= 18) return Level.Yellow;
                if (Experience <= 42) return Level.Orange;
                return Level.Red;
            }
        }

        public int Experience { get; private set; }
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

        public IList<Equipment> Inventory { get; private set; }
        public GameNotifier Notifier { get; set; }

        public const int ActionsPerTurn = 3;
        public const int MaxWounds = 2;
        private int MaxCarryingCapacity = 5;
        private int MaxItemsInHand = 2;

        public Survivor(string name, int initialExperience = 0)
        {
            Name = name;
            Experience = initialExperience;
            Inventory = new List<Equipment>();
        }

        public void ProcessTurn()
        {

        }

        public void KilledZombie()
        {
            var levelBefore = Level;
            Experience++;
            var levelAfter = Level;

            if (levelAfter != levelBefore)
            {
                if (Notifier != null) Notifier.Notify($"{Name} has leveled up  and is now {Level}");
            }
        }

        public (bool isEquipmentDropped, Equipment droppedEquipment) SustainInjury(int numberOfWounds)
        {
            Wounds = Wounds + numberOfWounds <= MaxWounds
                ? Wounds + numberOfWounds
                : MaxWounds;

            if (IsAlive)
            {
                if (Notifier != null) Notifier.Notify($"{Name} has been wounded!");
            }
            else
            {
                if (Notifier != null) Notifier.Notify($"{Name} has been killed!");
            }

            Equipment droppedEquipment = null;
            var isEquipmentDropped = false;

            // If the number of items carried exceeds the new Carrying Capacity
            // then drop and random item ...
            if (Inventory.Count > CarryingCapacity)
            {
                var rnd = new Random(DateTime.Now.Millisecond);
                var randomItem = rnd.Next(0, Inventory.Count - 1);

                droppedEquipment = DropItem(randomItem);
                isEquipmentDropped = true;
            }

            return (isEquipmentDropped, droppedEquipment);
        }

        public bool PickUpItem(Equipment equipment)
        {
            if (Inventory.Count < CarryingCapacity)
            {
                if (Inventory.Where(i => i.InHand).Count() < MaxItemsInHand)
                {
                    equipment.InHand = true;
                }

                Inventory.Add(equipment);
                if (Notifier != null) Notifier.Notify($"{Name} picks up a piece of equipment ({equipment.Name})");
                
                return true;
            }

            return false;
        }

        public Equipment DropItem(int inventoryIndex)
        {
            Equipment droppedEquipment = null;

            if (inventoryIndex < Inventory.Count)
            {
                droppedEquipment = Inventory[inventoryIndex];
                droppedEquipment.InHand = false;

                Inventory.RemoveAt(inventoryIndex);
                if (Notifier != null) Notifier.Notify($"{Name} drops a piece of equipment ({droppedEquipment.Name})");
            }

            return droppedEquipment;
        }
    }
}
