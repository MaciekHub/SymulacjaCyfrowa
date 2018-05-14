using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymulacjaCyfrowa
{
    class Program
    {
        static void Main(string[] args)
        {
            //INITIALIZING PARAMETRES
            int clock = 0; //System time
            int systemTime = 10000; //How long we want to simulate system
            Process current;
            EventList scheduler = new EventList();
            PatientService patientService = new PatientService();
            DonationPoint donationPoint = new DonationPoint();
            BloodBank bloodBank = new BloodBank();
            Donor donor = new Donor();
            Accident accident = new Accident();
            //patientService.Queue.FillQueue(15);
            //bloodBank.FillBloodList(1);
            bloodBank.Utilization.Activate(300, ref clock, scheduler); //planning utilization
            donationPoint.BloodNeeded = patientService.Queue.BloodQueueDemand;
            patientService.Activate(0, ref clock, scheduler);
            donor.Activate(10, ref clock, scheduler);
            accident.Activate(400, ref clock, scheduler);
            Console.WriteLine("NUMBER OF EVENTS: " + scheduler.eventList.Count + "///////////////////////////");
            bloodBank.SortBloodList();
            bloodBank.ShowBloodList();
            Console.WriteLine(bloodBank.BloodLevel);
            Console.WriteLine();
            patientService.Queue.ShowQueue();
            Console.WriteLine();
            bloodBank.phase = 1;

            Console.WriteLine("Wybierz tryb pracy symulatora:");
            Console.WriteLine("1. Ciagly");
            Console.WriteLine("2. Krokowy");

            bool check = true;
            string line = Console.ReadLine();
            int type = int.Parse(line);

            if (type == 1 || type == 2)
            {
                while (clock <= systemTime)
                {
                    Console.Write("//////// NEW PROCESS ////////");
                    current = scheduler.GetFirst().Process;
                    clock = scheduler.GetFirst().EventTime;
                    Console.WriteLine();
                    Console.WriteLine("SYSTEM TIME: " + clock);
                    scheduler.RemoveFirst();
                    Console.WriteLine("NUMBER OF PROCESSES: " + scheduler.eventList.Count);
                    current.Execute(bloodBank, donationPoint, scheduler, ref clock, patientService);
                    Console.WriteLine("/////////////////////////// \n");
                    if (type != 2) continue;
                    Console.WriteLine("Wcisnij dowolny klawisz");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("Nie ma takiej opcji!");
                check = false;
            }


            if (!check) return;
            Console.WriteLine();
            patientService.Queue.ShowQueue();
            bloodBank.ShowBloodList();
            Console.WriteLine("Blood queue demand: " + patientService.Queue.BloodQueueDemand);
            Console.WriteLine("Blood level: " + bloodBank.BloodLevel);
            Console.WriteLine("SYSTEM TIME: " + clock);
            Console.WriteLine("NUMBER OF PRECESSES LEFT: " + scheduler.eventList.Count);
            Console.WriteLine("SIMPLE ORDERS: " + donationPoint.SimOrderCounter);
            Console.WriteLine("EMERGENCY ORDERS: " + donationPoint.EmOrderCounter);
            Console.WriteLine("ACCIDENTS: " + donationPoint.AccidentCounter);
            Console.WriteLine("ACCIDENT DELAYS: " + donationPoint.AccidentDeniedCounter);
            Console.WriteLine("DONORS: " + donationPoint.DonorCounter);
            Console.WriteLine("PATIENTS: " + donationPoint.PatientCounter);
            Console.WriteLine("BLOOD ADDED: " + bloodBank.BloodAdded);
            Console.WriteLine("BLOOD TAKEN: " + bloodBank.BloodTaken);
            Console.WriteLine("UTILIZED UNITS: " + donationPoint.UtilizedUnits);
        }
    }
}
