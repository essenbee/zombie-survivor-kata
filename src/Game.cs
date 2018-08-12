using System;
using System.Collections.Generic;
using System.Linq;

namespace ZombieSurvivor.Core
{
    public class Game : INotifiable
    {
        public IList<GameEvent> GameHistory { get; set; }
        public IList<Survivor> Survivors { get; set; }
        public bool IsEndOfGame => (Survivors.Count > 0 && !Survivors.Any(s => s.IsAlive));
        public bool IsMissionAccomplished { get; set; }
        public Level Level
        {
            get
            {
                if (Survivors.Any())
                {
                    return Survivors
                        .Where(s => s.Experience == Survivors.Max(e => e.Experience))
                        .First()
                        .Level;
                }

                return Level.Blue;
            }
        }

        private GameNotifier _notifier;
        private int _gameRound = 0;

        public Game()
        {
            Survivors = new List<Survivor>();
            _notifier = new GameNotifier(this);

            var gameStart = DateTime.Now;

            GameHistory = new List<GameEvent>
            {
                new GameEvent
                {
                    EventDetail = $"New game started at {gameStart.ToShortTimeString()}"
                }
            };
        }

        public bool GameRound()
        {
            var gameLevelAtBeginningOfRound = Level;
            _gameRound++;
            Notify($"Starting round {_gameRound}");

            // Player Phase: Run through all of the living Survivors to execute their turns ...
            foreach (var survivor in Survivors.Where(s => s.IsAlive))
            {
                survivor.ProcessTurn();
                RecordAnyGameLevelChange(gameLevelAtBeginningOfRound);
            }

            // Zombie Phase: zombies attack and/or move

            if (IsEndOfGame)
            {
                Notify($"The game has ended, all Survivors died. The game lasted {_gameRound} rounds");
                return false;
            }

            if (IsMissionAccomplished)
            {
                Notify($"The Survivors have achieved their objective! They won the game in {_gameRound} rounds");
                return false;
            }

            // End Phase: cancel all Noise

            return true;
        }

        public void RecordAnyGameLevelChange(Level gameLevelAtBeginningOfRound)
        {
            if (Level != gameLevelAtBeginningOfRound)
            {
                Notify($"The game level is now {Level}!");
            }
        }

        public bool AddSurvivorToGame(Survivor newSurvivor)
        {
            var retVal = false;
            var newSurvivorName = newSurvivor.Name;

            if (!Survivors.Any(s => s.Name.Equals(newSurvivorName)))
            {
                newSurvivor.Notifier = _notifier;
                Survivors.Add(newSurvivor);
                Notify($"Survivor {newSurvivor.Name} was added to the game");

                retVal = true;
            }

            return retVal;
        }

        public void Notify(string eventDetail)
        {
            if (GameHistory is null)
            {
                GameHistory = new List<GameEvent>();
            }

            GameHistory.Add(new GameEvent
            {
                EventDetail = eventDetail
            });
        }
    }
}
