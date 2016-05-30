using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldSim
{
    class ScienceEvent : WorldEvent
    {
        public int DaysResearched { get; set; }
        public ScienceEvent(string name, string description, int completionTime)
        {
            this.Name = name;
            this.Description = description;
            this.CompletionTime = completionTime;
        }
    }
}
