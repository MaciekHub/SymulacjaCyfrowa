using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymulacjaCyfrowa
{
    internal class EventList 
    {
        public List<Event> eventList = new List<Event>();
       

        public Event GetFirst()
        {
            return eventList.First();
        }

        public void RemoveFirst()
        {
            eventList.RemoveAt(0);
        }

        public void ScheduleProcess(Event myEvent)
        {
            eventList.Add(myEvent);
            eventList = (from s in eventList orderby s.EventTime ascending select s).ToList();
        }


        public void FillEventList(int n)
        {
            Random random = new Random();
            for(int i=0; i<n; i++)
            {
                Event newEvent = new Event(new PatientService());
                eventList.Add(newEvent);
            }
        }

        public void AddEvent(PatientService patientService)
        {
            Event newEvent = new Event(patientService);
            eventList.Add(newEvent);
        }

        public void ShowEventList()
        {
            foreach(Event temp in eventList)
            {
                Console.Write("Event time:" + temp.EventTime + " ");
            }
        }


    }
}
