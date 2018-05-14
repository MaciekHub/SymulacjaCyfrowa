using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymulacjaCyfrowa
{
    internal class Event
    {
        private int _eventTime;
        public Process Process;
        
        public int EventTime { get => _eventTime; set => _eventTime = value; }
        
        public Event(Process processObject)
        {
            Process = processObject; 
        }
        
    }
}
