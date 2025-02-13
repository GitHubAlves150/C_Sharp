using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indexadores_e_Membros_const_readonly
{
    class AdCalcADC
    {
        private static double K_adc = 0.00488758553;
        private readonly double volts;

        public AdCalcADC(double volts)
        {
            this.volts = (volts >=0.0 && volts <=5.0)? volts : 2.5;
        }
        public int AdcValue()
        {
            return (int)(volts/ K_adc);
        }

    }
}
