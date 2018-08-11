using System;

namespace ZombieSurvivor.Core
{
    public class GameEvent
    {
        public DateTime EventDateTime { get; }
        public string EventDetail { get; set; }

        public GameEvent()
        {
            EventDateTime = DateTime.Now;
        }
    }
}
