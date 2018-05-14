using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymulacjaCyfrowa
{
    internal class PatientService : Process
    {
        private PatientQueue _queue = new PatientQueue();

        internal PatientQueue Queue { get => _queue; set => _queue = value; }

        //      internal PatientQueue Queue { get => _queue; set => _queue = value; }

        public override void Execute(BloodBank bloodBank, DonationPoint donationPoint, EventList scheduler, ref int clock, PatientService patientService)
        {
            bool active = true;
            Random random = new Random();

            while (active)
            {
                switch (phase)
                {
                    case 0:

                        // Planning next patient
                        int processTime = random.Next(20, 30);
                        Console.WriteLine("NEW PATIENT, next patient in about " + processTime + " units of time");
                        Activate(processTime, ref clock, scheduler);
                        donationPoint.PatientCounter++;
                        //

                        Queue.AddToQueue();
                        Console.WriteLine("Adding patient to queue");
                        Console.WriteLine("PATIENS IN QUEUE: " + Queue.patientQueue.Count);
                        Console.WriteLine("BLOOD LEVEL: " + bloodBank.BloodLevel + " // BLOOD WANTED BY QUEUE: " + Queue.BloodQueueDemand);

                        phase = 1;

                        if (donationPoint.EmD)
                        {
                            phase = 0;
                            active = false;
                        }
                        break;

                    case 1:

                        bloodBank.Activate(0, ref clock, scheduler);   //active other process to check blood if needed

                        if ((Queue.patientQueue.Count != 0) && (bloodBank.BloodLevel >= Queue.BloodQueueDemand))
                        {
                            Console.WriteLine("Blood transfuzion");
                            bloodBank.Transfuzion(Queue);
                            Console.WriteLine("Removing patient from queue");
                            Console.WriteLine("PATIENS IN QUEUE: " + Queue.patientQueue.Count);
                            Console.WriteLine("BLOOD LEVEL: " + bloodBank.BloodLevel);
                            bloodBank.ShowBloodList();
                        }


                        if ((Queue.patientQueue.Count != 0)  && (bloodBank.BloodLevel >= Queue.BloodQueueDemand))
                        {
                            Console.WriteLine("All conditions are checked: redo this process");
                            Activate(0, ref clock, scheduler);
                            phase = 1;
                        }
                        else
                        {
                            Console.WriteLine("End of process - PatientService");
                            phase = 0;
                        }

                        active = false;
                        break;              
                }
            }
        }


    }
}
