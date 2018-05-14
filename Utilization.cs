using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymulacjaCyfrowa
{
    internal class Utilization : Process
    {

        public override void Execute(BloodBank bloodBank, DonationPoint donationPoint, EventList scheduler,
            ref int clock, PatientService patientService)
        {
            // Utilization
            foreach (Blood blood in bloodBank.BloodList.ToList())
            {
                if (clock < blood.ExpTime) continue;
                bloodBank.BloodLevel -= blood.BloodUnits;
                bloodBank.BloodTaken += blood.BloodUnits;
                donationPoint.UtilizedUnits += blood.BloodUnits;
                bloodBank.BloodList.Remove(blood);
                Console.WriteLine("Utilization of expired blood");
                Console.WriteLine("Decreasing blood level");
            }

        }



    }
}
    
