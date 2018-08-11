using System;
using System.Collections.Generic;
using System.Linq;

namespace ZombieSurvivor.Core
{
    public class Game
    {
        public IList<GameEvent> GameHistory { get; set; }
        public IList<Survivor> Survivors { get; set; }
        public bool IsEndOfGame => (Survivors.Count > 0 && !Survivors.Any(s => s.IsAlive));
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

        public Game()
        {
            Survivors = new List<Survivor>();
            var gameStart = DateTime.Now;

            GameHistory = new List<GameEvent>
            {
                new GameEvent
                {
                    EventDateTime = gameStart,
                    EventDetail = $"New game started at {gameStart.ToShortTimeString()}"
                }
            };
        }

        public bool AddSurvivorToGame(Survivor newSurvivor)
        {
            var retVal = false;
            var newSurvivorName = newSurvivor.Name;

            if (!Survivors.Any(s => s.Name.Equals(newSurvivorName)))
            {
                Survivors.Add(newSurvivor);
                SurvivorAddedEvent(newSurvivor);

                retVal = true;
            }

            return retVal;
        }

        private void SurvivorAddedEvent(Survivor survivor)
        {
            if (GameHistory is null)
            {
                GameHistory = new List<GameEvent>();
            }

            GameHistory.Add(new GameEvent
            {
                EventDateTime = DateTime.Now,
                EventDetail = $"Survivor {survivor.Name} was added to the game"
            });
        }
    }
}
