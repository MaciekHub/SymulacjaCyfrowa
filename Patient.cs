using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymulacjaCyfrowa
{
    internal class Patient
    {
        private int _bloodDemand;   //Amount of blood needed to transfuzion
        private readonly int _id;


        public Patient(int x, int id)
        {
            _id = id;
            _bloodDemand = x;
        }

        public int GetID { get => _id; }
        public int GetBloodDemand { get => _bloodDemand;}
        public int SetBloodDemand { set => _bloodDemand = value; }
    }
}
