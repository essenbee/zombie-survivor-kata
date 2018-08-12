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
        public void RecordGameStartEvent_OnStartup()
        {
            var game = new Game();

            Assert.AreEqual(1, game.GameHistory.Count());
            Assert.IsTrue(game.GameHistory[0].EventDetail.StartsWith("New game started"));
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
        public void SetGameNotifierOnSurvivor_WhenAddingNewSurvivor()
        {
            var game = new Game();
            var bill = new Survivor("Bill");
            var successfullyAddedSurvivor = game.AddSurvivorToGame(bill);

            Assert.IsNotNull(game.Survivors.FirstOrDefault(s => s.Name.Equals("Bill")));
            Assert.IsNotNull(game.Survivors.First(s => s.Name.Equals("Bill")).Notifier);
        }

        [TestMethod]
        public void RecordEvent_OnAddingSurvivor()
        {
            var game = new Game();
            var bill = new Survivor("Bill");
            game.AddSurvivorToGame(bill);
            var lastEvent = game.GameHistory.LastOrDefault();

            Assert.IsNotNull(lastEvent);
            Assert.IsTrue(lastEvent.EventDetail.StartsWith($"Survivor {bill.Name} was added to the game"));
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
        public void RecordEndOfGameEvent_GivenAllSurvivorsAreDead()
        {
            var game = new Game();
            var bill = new Survivor("Bill");
            var ben = new Survivor("Ben");
            game.AddSurvivorToGame(bill);
            game.AddSurvivorToGame(ben);

            bill.SustainInjury(2);
            ben.SustainInjury(2);

            var continueGame = game.GameRound();
            var lastEvent = game.GameHistory.LastOrDefault();

            Assert.IsFalse(continueGame);
            Assert.IsNotNull(lastEvent);
            Assert.IsTrue(lastEvent.EventDetail.StartsWith("The game has ended, all Survivors died"));
        }

        [TestMethod]
        public void RecordEquipmentPickedUpBySurvivorEvent()
        {
            var game = new Game();
            var survivor = new Survivor("Bill");
            game.AddSurvivorToGame(survivor);
            var equipment = new Equipment("Baseball bat");

            survivor.PickUpItem(equipment);
            var lastEvent = game.GameHistory.LastOrDefault();

            Assert.IsNotNull(lastEvent);
            Assert.IsTrue(lastEvent.EventDetail.StartsWith($"{survivor.Name} picks up a piece of equipment ({equipment.Name})"));
        }

        [TestMethod]
        public void RecordSurvivorLevelUpEvent()
        {
            var game = new Game();
            var survivor = new Survivor("Bill", 6);
            game.AddSurvivorToGame(survivor); ;
            survivor.KilledZombie();
            var newLevel = survivor.Level;

            var lastEvent = game.GameHistory.LastOrDefault();

            Assert.IsNotNull(lastEvent);
            Assert.IsTrue(lastEvent.EventDetail.StartsWith($"{survivor.Name} has leveled up  and is now {newLevel}"));
        }

        //[TestMethod]
        //public void RecordGameLevelUpEvent()
        //{
        //    var game = new Game();
        //    var bill = new Survivor("Bill", 6);
        //    var ben = new Survivor("Ben", 4);
        //    game.AddSurvivorToGame(bill);
        //    game.AddSurvivorToGame(ben);

        //    ben.KilledZombie();
        //    var newGameLevel = game.Level;

        //    var lastEvent = game.GameHistory.LastOrDefault();

        //    Assert.IsNotNull(lastEvent);
        //    Assert.IsTrue(lastEvent.EventDetail.StartsWith($"The game has level is now {newGameLevel}!"));
        //}

        [TestMethod]
        public void RecordSurvivorWoundedEvent()
        {
            var game = new Game();
            var survivor = new Survivor("Bill");
            game.AddSurvivorToGame(survivor);;
            survivor.SustainInjury(1);

            var lastEvent = game.GameHistory.LastOrDefault();

            Assert.IsNotNull(lastEvent);
            Assert.IsTrue(lastEvent.EventDetail.StartsWith($"{survivor.Name} has been wounded!"));
        }

        [TestMethod]
        public void RecordSurvivorKillededEvent()
        {
            var game = new Game();
            var survivor = new Survivor("Bill");
            game.AddSurvivorToGame(survivor); ;
            survivor.SustainInjury(2);

            var lastEvent = game.GameHistory.LastOrDefault();

            Assert.IsNotNull(lastEvent);
            Assert.IsTrue(lastEvent.EventDetail.StartsWith($"{survivor.Name} has been killed!"));
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
