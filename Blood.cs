using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SymulacjaCyfrowa
{
    internal class Blood
    {
        private int _expTime;
        private int _bloodUnits;


        public int BloodUnits
        {
            get => _bloodUnits;
            set => _bloodUnits = value;
        }

        public int ExpTime
        {
            get => _expTime;
            set => _expTime = value;
        }


        public Blood(int expTime, int bloodAmount)
        {
            _expTime = expTime;
            _bloodUnits = bloodAmount;
        }

    }
}
