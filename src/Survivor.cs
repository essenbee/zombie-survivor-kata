using System;
using System.Collections.Generic;
using System.Linq;

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
        public IList<Equipment> Inventory { get; private set; }

        public const int ActionsPerTurn = 3;
        public const int MaxWounds = 2;
        private int MaxCarryingCapacity = 5;
        private int MaxItemsInHand = 2;

        public Survivor(string name)
        {
            Name = name;
            Inventory = new List<Equipment>();
        }

        public (bool isEquipmentDropped, Equipment droppedEquipment) SustainInjury(int numberOfWounds)
        {
            Wounds = Wounds + numberOfWounds <= MaxWounds
                ? Wounds + numberOfWounds
                : MaxWounds;

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
            }

            return droppedEquipment;
        }
    }
}
