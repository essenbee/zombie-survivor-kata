using System;
using System.Diagnostics;

namespace ZombieSurvivor.Core
{
    [DebuggerDisplay("{EventDetail}")]
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
