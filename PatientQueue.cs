using System;
using System.Collections.Generic;

namespace SymulacjaCyfrowa
{
    internal class PatientQueue
    {
        public Queue<Patient> patientQueue = new Queue<Patient>();

        public int BloodQueueDemand { get; set; }

        public void AddToQueue()
        {
            Random random = new Random();
            int number = random.Next(1, 5);
            patientQueue.Enqueue(new Patient(number, random.Next(1000, 4000)));
            BloodQueueDemand += number;

        }

        public void RemoveFromQueue()
        {
            Patient patient = patientQueue.Dequeue();
            BloodQueueDemand -= patient.GetBloodDemand;
        }

        public void FillQueue(int n)
        {
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                int temp = random.Next(1, 5);
                patientQueue.Enqueue(new Patient(temp, random.Next(1000, 4000)));
                BloodQueueDemand += temp;              
            }
        }

        public void ShowQueue()
        {
            int i = 0;
            foreach (Patient patient in patientQueue)
            {
                Console.WriteLine("Patient" + patient.GetID + " bloodDemand:" + patient.GetBloodDemand);
                i++;
            }
        }

        public void AccidentPatient(int n)
        {
            Random random = new Random();
            patientQueue.Enqueue(new Patient(n, random.Next(1000, 4000)));
            BloodQueueDemand += n;
        }

    }
}