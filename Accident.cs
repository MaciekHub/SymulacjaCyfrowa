using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymulacjaCyfrowa
{
    internal class Accident : Process
    {
        public override void Execute(BloodBank bloodBank, DonationPoint donationPoint, EventList scheduler,
            ref int clock, PatientService patientService)
        {
            Random random = new Random();

            // Planning next accident
            var processTime = random.Next(400, 500);
            Console.WriteLine("Next accident in about " + processTime + " units of time");
            Activate(processTime, ref clock, scheduler);
            //

            // Accident
            if (bloodBank.BloodLevel > donationPoint.GetTK)
            {
                bloodBank.RemoveBlood(donationPoint.GetTK);
                Console.WriteLine("Take blood needed to accident");
                donationPoint.AccidentCounter++;
            }
            else
            {
                Console.WriteLine("Adding patient from accident to queue");
                patientService.Queue.AccidentPatient(donationPoint.GetTK);                    
                donationPoint.AccidentDeniedCounter++;
            }
            //

        }

    }
}
