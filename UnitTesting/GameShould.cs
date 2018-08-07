using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ZombieSurvivor.Core;

namespace UnitTests
{
    [TestClass]
    public class GameShould
    {
        [TestMethod]
        public void StartOutWithNoSurvivors()
        {
            var game = new Game();

            Assert.AreEqual(0, game.Survivors.Count());
        }

        [TestMethod]
        public void AddNewSurvivor_GivenNoCurrentSurvivors()
        {
            var game = new Game();
            var bill = new Survivor("Bill");
            var successfullyAddedSurvivor = game.AddSurvivorToGame(bill);

            Assert.IsTrue(successfullyAddedSurvivor);
            Assert.AreEqual(1, game.Survivors.Count());
        }

        [TestMethod]
        public void NotAddNewSurvivor_GivenSurvivorWithSameNameAlreadyInGame()
        {
            var game = new Game();
            var bill = new Survivor("Bill");
            game.AddSurvivorToGame(bill);

            var differentBill = new Survivor("Bill");
            var successfullyAddedSurvivor = game.AddSurvivorToGame(differentBill);

            Assert.IsFalse(successfullyAddedSurvivor);
        }
    }
}
