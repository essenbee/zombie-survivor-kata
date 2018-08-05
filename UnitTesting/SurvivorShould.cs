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

            Assert.AreEqual(0, survivor.EquipmentCarried.Count);
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
    }
}
