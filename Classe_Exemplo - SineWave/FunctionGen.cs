using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classe_Exemplo
{
    class FunctionGen
    {
        //fields
        private bool burst, sweep;
        private int startFrequency, stopFrequency;
        private short sweepTime, burstCycle, signalType;

        private Date firmwareDate;
        private PWM signalPWM;
        private SineWave signalSine;

        //construtor
        public FunctionGen(double offset, double frequency, double amplitude, bool argSweep, bool argBurst)
        {
            signalSine = new SineWave(offset, frequency, amplitude);
            sweep = argSweep;
            burst = argBurst;
            signalType = 1;                
        }

        public FunctionGen(double duty, double frequency, double amplitude)
        {
            signalPWM = new PWM(duty, frequency, amplitude);
            signalType = 2;
        }

        //methods
        public void setSwep(int startFreq, int stopFreq, short sweepTime)
        {
            if(startFreq < stopFreq)
            {
                startFrequency = (startFreq >=0 && startFreq <=9000)? startFreq:0;
                stopFrequency = (stopFreq >= 0 && stopFreq <= 1000) ? stopFreq : 1000;

            }
            else
            {
                startFreq = 0;
                stopFreq = 10000;
            }

            sweepTime = (sweepTime >= 1 && sweepTime<=100)? sweepTime :(short)0;
        }//end SetSweep
        public void SetBurst(short burstCyc)
        {
            burstCycle = (burstCyc >= 1 && burstCyc <=50) ? burstCyc : (short)1;
        }

        public void SetFirmwareDate(int year, short month, short Day)
        {
            firmwareDate = new Date(month, Day, year);
        }

        public void Report(string funcGen)
        {
            Console.WriteLine(" "+ funcGen);
            Console.WriteLine(" Signal Type:........" + VerifySignal()   );
            Console.WriteLine(" Sweep Start Frequency:........" + startFrequency +"Hz");
            Console.WriteLine(" Sweep Stop Frequency:........" + stopFrequency+ "Hz");
            Console.WriteLine(" Sweep Time:........" + sweepTime+"ms");
            Console.WriteLine(" Burst Cycle:........" + burstCycle +"Cycles");

            if(signalSine != null)
            {
                signalSine.Report("Sine");
            }
            if(signalPWM != null)
            {
                signalPWM.Report("PWM");
            }

            Console.WriteLine("Firmware Date: " +firmwareDate.ShowDate());
        }

        public string VerifySignal()
        {
            switch(signalType)
            {
                case 1:
                    return "Sine";
                case 2:
                    return "PWM";
                default:
                    return "unknow";
            }

        }
    }//end class FunctionGen
}//end namespace
