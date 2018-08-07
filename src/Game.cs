using System.Collections.Generic;
using System.Linq;

namespace ZombieSurvivor.Core
{
    public class Game
    {
        public IList<Survivor> Survivors { get; set; }
        public bool IsEndOfGame => (Survivors.Count > 0 && !Survivors.Any(s => s.IsAlive));

        public Game()
        {
            Survivors = new List<Survivor>();
        }

        public bool AddSurvivorToGame(Survivor newSurvivor)
        {
            var retVal = false;
            var newSurvivorName = newSurvivor.Name;

            if (!Survivors.Any(s => s.Name.Equals(newSurvivorName)))
            {
                Survivors.Add(newSurvivor);
                retVal = true;
            }

            return retVal;
        }
    }
}
