using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classe_Exemplo
{
    class SineWave
    {
        //fields
        private double offset, frequency, amplitude;

        //construtors
        public SineWave(double argOffset, double argFrequency, double argAmplitude)
        {
            SetSineWave(argOffset, argFrequency, argAmplitude);
        }

        public SineWave(double argOffset)
        {
            SetSineWave(argOffset, 1000.0, 5.0);
        }
        public SineWave(double argOffset, double argFrequency)
        {
            SetSineWave(argOffset, argFrequency, 5.0);
        }
        public SineWave(SineWave sinewave)
        {
            SetSineWave(sinewave.offset, sinewave.frequency, sinewave.amplitude);
        }

        public void SetSineWave(double argOffset, double argFrequency, double argAmplitude)
        {
            Offset = argOffset;
            Frequency = argFrequency;
            Amplitude = argAmplitude;
        }

        public void Report(string sineName)
        {
            Console.WriteLine(" " + sineName + " signal is configures to: ");
            Console.WriteLine(" Offset:    " + offset + "V");
            Console.WriteLine(" Frequency: " + frequency + "Hz");
            Console.WriteLine(" Amplitude: " + amplitude + " V");

        }

        public double Offset
        {
            get { return offset; }
            set { offset = (value >= -5.0 && value <= -5.0) ? value: 0.0; }
        }
        public double Frequency
        {
            get { return frequency; }
            set { frequency = (value >= 0.0 && value <= 10.0E3) ? value : 1000.0; }
        }
        public double Amplitude
        {
            get { return amplitude; }
            set { amplitude = (value >= 3.3 && value <= 12.0) ? value : 5.0; }
        }

    }
}
