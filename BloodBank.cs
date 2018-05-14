using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SymulacjaCyfrowa
{
    internal class BloodBank : Process
    {

        public int BloodLevel { get; set; } = 0;
        // variables implemented to show ending statistics
        private int _bloodAdded = 0;
        private int _bloodTaken = 0;


        internal List<Blood> BloodList { get; set; } = new List<Blood>();
        internal Utilization Utilization { get; set; } = new Utilization();
        public int BloodAdded
        {
            get => _bloodAdded;
            set => _bloodAdded = value;
        }
        public int BloodTaken
        {
            get => _bloodTaken;
            set => _bloodTaken = value;
        }


        public override void Execute(BloodBank bloodBank, DonationPoint donationPoint, EventList scheduler,
            ref int clock, PatientService patientService)
        {

            Console.WriteLine("Checking state of blood");

            //simple order
            if (bloodBank.BloodLevel <= donationPoint.GetR && !donationPoint.SimD)
            {
                donationPoint.Activate(0, ref clock, scheduler);
                donationPoint.phase = 0;
            }
            //


            //emergency order
            if (bloodBank.BloodLevel < patientService.Queue.BloodQueueDemand && !donationPoint.EmD)
            {
                donationPoint.Activate(0, ref clock, scheduler);
                donationPoint.phase = 1;
            }
            //
        }



        public void AddBlood(int expTime, int bloodUnits)
        {
            BloodList.Add(new Blood(expTime, bloodUnits));
            BloodLevel += bloodUnits;
            SortBloodList();
            BloodAdded += bloodUnits;
        }


        public void SortBloodList()
        {
            BloodList = BloodList.OrderBy(o => o.ExpTime).ToList();
        }


        public void Transfuzion(PatientQueue patientQueue)
        {
            Patient patient = patientQueue.patientQueue.Peek();
            int temp = patient.GetBloodDemand;

            foreach(Blood blood in BloodList.ToList())
            {
                if (blood.BloodUnits > temp)
                {
                    blood.BloodUnits -= temp;
                    break;
                }
                else
                {
                    temp -= blood.BloodUnits;
                    BloodList.Remove(blood);
                }
            }
            patientQueue.BloodQueueDemand -= patient.GetBloodDemand;
            BloodTaken += patient.GetBloodDemand;
            BloodLevel -= patient.GetBloodDemand;
            patient.SetBloodDemand = 0;
            patientQueue.RemoveFromQueue();
        }

        public void RemoveBlood(int n)
        {
            int temp = n;

            foreach (Blood blood in BloodList.ToList())
            {
                if (blood.BloodUnits > temp)
                {
                    blood.BloodUnits -= temp;
                    break;
                }
                else
                {
                    temp -= blood.BloodUnits;
                    BloodList.Remove(blood);
                }
            }
            BloodLevel -= n;
            BloodTaken += n;
        }

        public void ShowBloodList()
        {
            foreach(Blood blood in BloodList)
            {
                Console.WriteLine("expTime: " + blood.ExpTime + " units: " + blood.BloodUnits);
            }
        }

        public void FillBloodList(int n)
        {
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                int temp = random.Next(1, 10);
                BloodList.Add(new Blood(300, temp));
                BloodLevel += temp;
                BloodAdded += temp;
            }

        }

    }
}
