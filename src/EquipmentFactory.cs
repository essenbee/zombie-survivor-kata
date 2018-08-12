using System;
using System.Collections.Generic;
using System.Text;

namespace ZombieSurvivor.Core
{
    public class EquipmentFactory
    {
        public Equipment GetEquipment(EquipmentType type)
        {
            switch (type)
            {
                case EquipmentType.BaseballBat:
                    return new Equipment("Baseball bat", true, false, false, 0, 1, 4, 1);
                case EquipmentType.Katana:
                    return new Equipment("Katana", true, false, false, 0, 2, 3, 2);
                case EquipmentType.FireAxe:
                    return new Equipment("Fire axe", true, true, true, 0, 1, 4, 2);
                case EquipmentType.CrowBar:
                    return new Equipment("Crowbar", true, true, false, 0, 1, 4, 1);
                case EquipmentType.BottledWater:
                    return new Equipment("Bottled water", false, false, false, 0, 0, 0, 0);
                case EquipmentType.Chainsaw:
                    return new Equipment("Chainsaw", true, true, true, 0, 5, 5, 2);
                case EquipmentType.Pistol:
                    return new Equipment("Pistol", true, false, true, 1, 1, 4, 1);
                case EquipmentType.Rifle:
                    return new Equipment("Rifle", true, false, true, 3, 1, 3, 1);
                default:
                    return null;
            }
        }
    }
}
