using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymulacjaCyfrowa
{
    internal class Process
    {

        public int phase;

        public Process()
        {
            phase = 0;
        }

        public void Activate(int activationTime, ref int clock, EventList eventList)
        {
            Event @event = new Event(this);
            @event.EventTime = clock + activationTime;
            eventList.ScheduleProcess(@event);
        }

        public virtual void Execute(BloodBank bloodBank, DonationPoint donationPoint, EventList eventList, ref int clock, PatientService patientService)
        {
        }


    }
}
