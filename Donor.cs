using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymulacjaCyfrowa
{
    internal class Donor : Process
    {
        public override void Execute(BloodBank bloodBank, DonationPoint donationPoint, EventList scheduler,
            ref int clock, PatientService patientService)
        {

            //planning next donor
            Random random = new Random();
            int processTime;
            processTime = random.Next(50, 100);
            Console.WriteLine("NEW DONOR, next donor in about " + processTime + " units of time");
            Activate(processTime, ref clock, scheduler);
            //

            //adding blood to bank
            Console.WriteLine("Increased blood level by 1 unit");
            bloodBank.AddBlood(expTime: clock + donationPoint.GetT2, bloodUnits: 1);
            bloodBank.ShowBloodList();
            Console.WriteLine(bloodBank.BloodLevel);
            Console.WriteLine("Adding blood to list");
            donationPoint.DonorCounter++;
            //

            // Planning time of utilization
            Console.WriteLine("Utilization in about " + donationPoint.GetT2 + " units of time");
            bloodBank.Utilization.Activate(donationPoint.GetT2, ref clock, scheduler);
            //

        }


    }

}
