using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZombieSurvivor.Core;

namespace UnitTests
{
    [TestClass]
    public class SurvivorShould
    {
        [TestMethod]
        public void StartOutWithNoWounds()
        {
            var survivor = new Survivor("Bill");

            Assert.AreEqual(0, survivor.Wounds);
        }

        [TestMethod]
        public void StartOutAlive()
        {
            var survivor = new Survivor("Bill");

            Assert.IsTrue(survivor.IsAlive);
        }

        [TestMethod]
        public void StartOutWithThreeActions()
        {
            var survivor = new Survivor("Bill");

            Assert.AreEqual(3, survivor.ActionsRemaining);
        }

        [TestMethod]
        public void StartOutWithName()
        {
            var survivor = new Survivor("Bill");

            Assert.IsNotNull(survivor.Name);
        }

        [TestMethod]
        public void StartOutWithNoEquipment()
        {
            var survivor = new Survivor("Bill");

            Assert.AreEqual(0, survivor.Inventory.Count);
        }

        [TestMethod]
        public void StartOutWithMaximumCarryingCapacity()
        {
            var survivor = new Survivor("Bill");

            Assert.AreEqual(5, survivor.CarryingCapacity);
        }

        [TestMethod]
        public void TakingWoundsReducesCarryingCapacity_GivenAtFullHeath()
        {
            var survivor = new Survivor("Bill");
            survivor.SustainInjury(1);

            Assert.AreEqual(4, survivor.CarryingCapacity);
        }

        [TestMethod]
        public void TakeOneWoundAndNotDie_GivenAtFullHeath()
        {
            var survivor = new Survivor("Bill");
            survivor.SustainInjury(1);

            Assert.AreEqual(1, survivor.Wounds);
            Assert.IsTrue(survivor.IsAlive);
        }

        [TestMethod]
        public void TakeTwoWoundsAndDie_GivenAtFullHeath()
        {
            var survivor = new Survivor("Bill");
            survivor.SustainInjury(1);
            survivor.SustainInjury(1);

            Assert.AreEqual(2, survivor.Wounds);
            Assert.IsFalse(survivor.IsAlive);
        }

        [TestMethod]
        public void NeverHaveMoreThanTwoWounds()
        {
            var survivor = new Survivor("Bill");
            survivor.SustainInjury(1);
            survivor.SustainInjury(1);
            survivor.SustainInjury(1);

            Assert.AreEqual(2, survivor.Wounds);
            Assert.IsFalse(survivor.IsAlive);
        }

        [TestMethod]
        public void EquipmentIsAddedToInventory_GivenSufficientCapacity()
        {
            var survivor = new Survivor("Bill");
            var baseballBat = new Equipment("Baseball bat");

            var isPickedUp = survivor.PickUpEquipment(baseballBat);

            Assert.IsTrue(survivor.Inventory.Contains(baseballBat));
            Assert.IsTrue(isPickedUp);
        }

        [TestMethod]
        public void EquipmentIsNotAddedToInventory_GivenNoCapacity()
        {
            var survivor = new Survivor("Bill");
            var baseballBat = new Equipment("Baseball bat");
            var katana = new Equipment("Katana");
            var pistol = new Equipment("Pistol");
            var bottledWater = new Equipment("Bottled water");
            var fryingPan = new Equipment("Frying pan");
            var molotov = new Equipment("Molotov");

            survivor.PickUpEquipment(katana);
            survivor.PickUpEquipment(pistol);
            survivor.PickUpEquipment(bottledWater);
            survivor.PickUpEquipment(fryingPan);
            survivor.PickUpEquipment(molotov);

            var isPickedUp = survivor.PickUpEquipment(baseballBat);

            Assert.IsFalse(survivor.Inventory.Contains(baseballBat));
            Assert.IsFalse(isPickedUp);
        }

        [TestMethod]
        public void DropAnItemWhenWounded_GivenFiveItemsCarried()
        {
            var survivor = new Survivor("Bill");

            var baseballBat = new Equipment("Baseball bat");
            var katana = new Equipment("Katana");
            var pistol = new Equipment("Pistol");
            var bottledWater = new Equipment("Bottled water");
            var fryingPan = new Equipment("Frying pan");

            survivor.PickUpEquipment(baseballBat);
            survivor.PickUpEquipment(katana);
            survivor.PickUpEquipment(pistol);
            survivor.PickUpEquipment(bottledWater);
            survivor.PickUpEquipment(fryingPan);
            
            var result = survivor.SustainInjury(1);

            Assert.IsTrue(result.isEquipmentDropped);
            Assert.IsNotNull(result.droppedEquipment);
            Assert.AreEqual(4, survivor.CarryingCapacity);
        }
    }
}
