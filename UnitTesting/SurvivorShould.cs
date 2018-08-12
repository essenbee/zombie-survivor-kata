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
        public void StartOutLevelBlue()
        {
            var survivor = new Survivor("Bill");

            Assert.AreEqual(Level.Blue, survivor.Level);
        }

        [TestMethod]
        public void StartOutWithNoExperience()
        {
            var survivor = new Survivor("Bill");

            Assert.AreEqual(0, survivor.Experience);
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
            var baseballBat = EquipmentFactory.GetEquipment(EquipmentType.BaseballBat);

            var isPickedUp = survivor.PickUpItem(baseballBat);

            Assert.IsTrue(survivor.Inventory.Contains(baseballBat));
            Assert.IsTrue(isPickedUp);
        }

        [TestMethod]
        public void FirstItemPickedUpIsInHand_GivenNoItemsInInventory()
        {
            var survivor = new Survivor("Bill");
            var baseballBat = EquipmentFactory.GetEquipment(EquipmentType.BaseballBat);

            var isPickedUp = survivor.PickUpItem(baseballBat);

            Assert.IsTrue(survivor.Inventory.Contains(baseballBat));
            Assert.IsTrue(baseballBat.InHand);
        }

        [TestMethod]
        public void SecondItemPickedUpIsInHand_GivenNoItemsInInventory()
        {
            var survivor = new Survivor("Bill");
            var baseballBat = EquipmentFactory.GetEquipment(EquipmentType.BaseballBat);
            var katana = EquipmentFactory.GetEquipment(EquipmentType.Katana);

            survivor.PickUpItem(baseballBat);
            survivor.PickUpItem(katana);

            Assert.IsTrue(survivor.Inventory.Contains(baseballBat));
            Assert.IsTrue(survivor.Inventory.Contains(katana));
            Assert.IsTrue(baseballBat.InHand);
            Assert.IsTrue(katana.InHand);
        }

        [TestMethod]
        public void ThirdItemPickedUpIsInReserve_GivenNoItemsInInventory()
        {
            var survivor = new Survivor("Bill");
            var baseballBat = EquipmentFactory.GetEquipment(EquipmentType.BaseballBat);
            var katana = EquipmentFactory.GetEquipment(EquipmentType.Katana);
            var bottledWater = EquipmentFactory.GetEquipment(EquipmentType.BottledWater);

            survivor.PickUpItem(baseballBat);
            survivor.PickUpItem(katana);
            survivor.PickUpItem(bottledWater);

            Assert.IsTrue(survivor.Inventory.Contains(baseballBat));
            Assert.IsTrue(survivor.Inventory.Contains(katana));
            Assert.IsTrue(survivor.Inventory.Contains(bottledWater));
            Assert.IsTrue(baseballBat.InHand);
            Assert.IsTrue(katana.InHand);
            Assert.IsFalse(bottledWater.InHand);
        }

        [TestMethod]
        public void EquipmentIsNotAddedToInventory_GivenNoCapacity()
        {
            var survivor = new Survivor("Bill");
            var baseballBat = EquipmentFactory.GetEquipment(EquipmentType.BaseballBat);
            var katana = EquipmentFactory.GetEquipment(EquipmentType.Katana);
            var pistol = EquipmentFactory.GetEquipment(EquipmentType.Pistol);
            var bottledWater = EquipmentFactory.GetEquipment(EquipmentType.BottledWater);
            var fryingPan = EquipmentFactory.GetEquipment(EquipmentType.FireAxe);
            var molotov = EquipmentFactory.GetEquipment(EquipmentType.CrowBar);

            survivor.PickUpItem(katana);
            survivor.PickUpItem(pistol);
            survivor.PickUpItem(bottledWater);
            survivor.PickUpItem(fryingPan);
            survivor.PickUpItem(molotov);

            var isPickedUp = survivor.PickUpItem(baseballBat);

            Assert.IsFalse(survivor.Inventory.Contains(baseballBat));
            Assert.IsFalse(isPickedUp);
        }

        [TestMethod]
        public void DropAnItemWhenWounded_GivenFiveItemsCarried()
        {
            var survivor = new Survivor("Bill");

            var baseballBat = EquipmentFactory.GetEquipment(EquipmentType.BaseballBat);
            var katana = EquipmentFactory.GetEquipment(EquipmentType.Katana);
            var pistol = EquipmentFactory.GetEquipment(EquipmentType.Pistol);
            var bottledWater = EquipmentFactory.GetEquipment(EquipmentType.BottledWater);
            var fryingPan = EquipmentFactory.GetEquipment(EquipmentType.FireAxe);

            survivor.PickUpItem(baseballBat);
            survivor.PickUpItem(katana);
            survivor.PickUpItem(pistol);
            survivor.PickUpItem(bottledWater);
            survivor.PickUpItem(fryingPan);
            
            var result = survivor.SustainInjury(1);

            Assert.IsTrue(result.isEquipmentDropped);
            Assert.IsNotNull(result.droppedEquipment);
            Assert.AreEqual(4, survivor.CarryingCapacity);
            Assert.IsFalse(result.droppedEquipment.InHand);
        }

        [TestMethod]
        public void NoItemDroppedWhenWounded_GivenSufficentCarryingCapacity()
        {
            var survivor = new Survivor("Bill");

            var baseballBat = EquipmentFactory.GetEquipment(EquipmentType.BaseballBat);
            var katana = EquipmentFactory.GetEquipment(EquipmentType.Katana);
            var pistol = EquipmentFactory.GetEquipment(EquipmentType.Pistol);

            survivor.PickUpItem(baseballBat);
            survivor.PickUpItem(katana);
            survivor.PickUpItem(pistol);

            var result = survivor.SustainInjury(1);

            Assert.IsFalse(result.isEquipmentDropped);
        }

        [TestMethod]
        public void GainOneExperience_GivenZombieKilled()
        {
            var survivor = new Survivor("Bill");
            survivor.KilledZombie();

            Assert.AreEqual(1, survivor.Experience);
        }

        [TestMethod]
        public void BeLevelBlue_GivenSixExperience()
        {
            var survivor = new Survivor("Bill", 6);

            Assert.AreEqual(Level.Blue, survivor.Level);
        }

        [TestMethod]
        public void BeLevelYellow_GivenSevenExperience()
        {
            var survivor = new Survivor("Bill", 7);
            
            Assert.AreEqual(Level.Yellow, survivor.Level);
        }

        [TestMethod]
        public void BeLevelOrangeGivenNineteenExperience()
        {
            var survivor = new Survivor("Bill", 19);

            Assert.AreEqual(Level.Orange, survivor.Level);
        }

        [TestMethod]
        public void BeLevelRed_GivenFortyThreeExperience()
        {
            var survivor = new Survivor("Bill", 43);

            Assert.AreEqual(Level.Red, survivor.Level);
        }
    }
}
