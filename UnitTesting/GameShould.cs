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
        public void NotTriggerEndOfGame_WhenGameStarts()
        {
            var game = new Game();

            Assert.IsFalse(game.IsEndOfGame);
        }

        [TestMethod]
        public void StartOutLevelBlue()
        {
            var game = new Game();

            Assert.AreEqual(Level.Blue, game.Level);
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
            Assert.AreEqual(1, game.Survivors.Count());
        }

        [TestMethod]
        public void NotFlagEndOfGame_GivenAtLeastOneLivingSurvivor()
        {
            var game = new Game();
            var bill = new Survivor("Bill");
            var ben = new Survivor("Ben");
            game.AddSurvivorToGame(bill);
            game.AddSurvivorToGame(ben);

            ben.SustainInjury(2);

            Assert.IsTrue(bill.IsAlive);
            Assert.IsFalse(ben.IsAlive);
            Assert.AreEqual(2, game.Survivors.Count());
            Assert.IsFalse(game.IsEndOfGame);
        }

        [TestMethod]
        public void FlagEndOfGame_GivenAllSurvivorsAreDead()
        {
            var game = new Game();
            var bill = new Survivor("Bill");
            var ben = new Survivor("Ben");
            game.AddSurvivorToGame(bill);
            game.AddSurvivorToGame(ben);

            bill.SustainInjury(2);
            ben.SustainInjury(2);

            Assert.IsFalse(bill.IsAlive);
            Assert.IsFalse(ben.IsAlive);
            Assert.AreEqual(2, game.Survivors.Count());
            Assert.IsTrue(game.IsEndOfGame);
        }

        [TestMethod]
        public void BeLevelBlue_GivenAllSurvivorsLevelBlue()
        {
            var game = new Game();
            var bill = new Survivor("Bill", 3);
            var ben = new Survivor("Ben", 6);

            game.AddSurvivorToGame(bill);
            game.AddSurvivorToGame(ben);

            Assert.AreEqual(Level.Blue, game.Level);
        }

        [TestMethod]
        public void BeLevelYellow_GivenHighestLevelSurvivorIsYellow()
        {
            var game = new Game();
            var bill = new Survivor("Bill", 15);
            var ben = new Survivor("Ben", 6);

            game.AddSurvivorToGame(bill);
            game.AddSurvivorToGame(ben);

            Assert.AreEqual(Level.Yellow, game.Level);
        }

        [TestMethod]
        public void BeLevelOrange_GivenHighestLevelSurvivorIsOrange()
        {
            var game = new Game();
            var bill = new Survivor("Bill", 20);
            var ben = new Survivor("Ben", 6);;
            var weed = new Survivor("Little Weed", 20);

            game.AddSurvivorToGame(bill);
            game.AddSurvivorToGame(ben);
            game.AddSurvivorToGame(weed);

            Assert.AreEqual(Level.Orange, game.Level);
        }

        [TestMethod]
        public void BeLevelRed_GivenHighestLevelSurvivorIsRed()
        {
            var game = new Game();
            var bill = new Survivor("Bill", 45);
            var ben = new Survivor("Ben", 6);
            var weed = new Survivor("Little Weed", 20);

            game.AddSurvivorToGame(bill);
            game.AddSurvivorToGame(ben);
            game.AddSurvivorToGame(weed);

            Assert.AreEqual(Level.Red, game.Level);
        }
    }
}
