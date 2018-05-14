using System;

namespace SymulacjaCyfrowa
{
    internal class DonationPoint : Process
    {
        private const int N = 20; //amount of blood to order in simple order
        private const int Q = 11; //amount of blood to order in emergency order
        private const int Z = 100; //avarage simple order delivery time
        private const int E = 30; //avarage emergency order delivery time
        private const int T1 = 300; //blood expire time
        private const int T2 = 100; //blood expire time
        private const int TK = 15; //amount of blood needed in accident
        private const int R = 15; //minimal level of blood allowed
        private int _emergencyDelivery; //variable that helps detecting emergrency order
        private int _simpleDelivery; //variable that helps detecting simple order


        private int _bloodNeeded; //current amount of blood requested by patients
        public bool EmD { get; set; } = false; //flag to see if emergency order is already in process
        public bool SimD { get; set; } = false; //flag to see if simple order is already in process

        // variables implemented to show ending statistics
        private int _simOrderCounter = 0;
        private int _emOrderCounter = 0;
        private int _accidentCounter = 0;
        private int _donorCounter = 0;
        private int _patientCounter = 0;
        private int _accidentDenied = 0;
        private int _utilizedUnits = 0;

        public int SimOrderCounter
        {
            get => _simOrderCounter;
            set => _simOrderCounter = value;
        }
        public int EmOrderCounter
        {
            get => _emOrderCounter;
            set => _emOrderCounter = value;
        }
        public int AccidentCounter
        {
            get => _accidentCounter;
            set => _accidentCounter = value;
        }
        public int DonorCounter
        {
            get => _donorCounter;
            set => _donorCounter = value;
        }
        public int PatientCounter
        {
            get => _patientCounter;
            set => _patientCounter = value;
        }
        public int AccidentDeniedCounter
        {
            get => _accidentDenied;
            set => _accidentDenied = value;
        }
        public int UtilizedUnits
        {
            get => _utilizedUnits;
            set => _utilizedUnits = value;
        }
        //

        public int BloodNeeded { get => _bloodNeeded; set => _bloodNeeded = value; }
        public int GetN { get => N; }
        public int GetQ { get => Q; }
        public int GetTK { get => TK; }
        public int GetZ { get => Z; }
        public int GetE { get => E; }
        public int GetT1 { get => T1; }
        public int GetT2 { get => T2; }
        public int GetR { get => R; }
        public int EmergencyDelivery { get => _emergencyDelivery; set => _emergencyDelivery = value; }
        public int SimpleDelivery { get => _simpleDelivery; set => _simpleDelivery = value; }

        public override void Execute(BloodBank bloodBank, DonationPoint donationPoint, EventList scheduler, ref int clock, PatientService patientService)
        {
            Random random = new Random();

            bool active = true;
            while (active)
                switch (phase)
                {
                    case 0:                        
                        Console.WriteLine("Simple order");
                        SimD = true;

                        // planning end of Delivery
                        Console.WriteLine("Simple order in about " + GetZ + " units of time");
                        Activate(GetZ, ref clock, scheduler);
                        //

                        SimpleDelivery = clock + GetZ;
                        phase = 2;
                        active = false;
                        break;

                    case 1:
                        Console.WriteLine("Emergency order");
                        EmD = true;

                        // planning end of Delivery
                        Console.WriteLine("Emergency order in about " + GetE + " units of time");
                        Activate(GetE, ref clock, scheduler);
                        //

                        EmergencyDelivery = clock + GetE;
                        phase = 2;
                        active = false;
                        break;

                    case 2:
                        if (clock == SimpleDelivery)
                        {
                            Console.WriteLine("Blood delivery from simple order");
                            bloodBank.AddBlood(clock + T1, GetN);

                            // Planning time of utilization
                            Console.WriteLine("Utilization in about " + GetT1 + " units of time");
                            bloodBank.Utilization.Activate(GetT1, ref clock, scheduler);
                            //

                            SimD = false;
                            SimOrderCounter++;

                        }
                        if (clock == EmergencyDelivery)
                        {
                            Console.WriteLine("Blood delivery from emergency order");
                            bloodBank.AddBlood(clock + GetT1, GetQ);

                            // Planning time of utilization
                            Console.WriteLine("Utilization in about " + GetT1 + " units of time");
                            bloodBank.Utilization.Activate(GetT1, ref clock, scheduler);
                            //

                            patientService.phase = 1;
                            EmD = false;
                            EmOrderCounter++;

                            // Waking up process
                            patientService.Activate(0, ref clock, scheduler);
                            //
                        }
                        active = false;
                        break;
                }
        }
    }
}