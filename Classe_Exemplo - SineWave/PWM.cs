using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classe_Exemplo
{
    class PWM
    {
        public PWM(double argDuty, double argFrequency, double argAmplitude)
        {
            SetPWM(argDuty, argFrequency, argAmplitude);
        }
        public PWM(double argDuty)
        {
            SetPWM(argDuty, 1000.0, 5.0);
        }
        public PWM(double argDuty, double argFrequency)
        {
            SetPWM(argDuty, argFrequency, 5.0);
        }
        public PWM(PWM pwm)
        {
            SetPWM(pwm.duty, pwm.frequency, pwm.amplitude);
        }

        private double duty;
        private double frequency;
        private double amplitude;


        public void SetPWM(double argDuty, double ArgFrequency, double argAmplitude)
        {
           /* duty = (argDuty >= 0.0 && argDuty <= 100.0) ? argDuty : 50.0;
            frequency = (ArgFrequency >= 0.0 && ArgFrequency <= 10.0E3) ? ArgFrequency : 1000.0;
            amplitude = (argAmplitude >= 3.3 && argAmplitude <= 15.0) ? argAmplitude : 5.0;
        */
           Duty = argDuty;
           Frequency = ArgFrequency;
           Amplitude = argAmplitude;
        }



        public void Report(string pwmName)
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine(" " + pwmName + " signal is configured to: ");
            Console.WriteLine(" Duty: " + duty + "%");
            Console.WriteLine(" Frequency: " + frequency + "Hz");
            Console.WriteLine(" Amplitude: " + amplitude + "V");
            Console.WriteLine("------------------------------------------");

        }
        public double Duty
        {
            get { return duty; }
            set { duty = (value >= 0.0 && value <= 100.0) ? value : 50.0; }
        }

        public double Frequency
        {
            get { return frequency; }
            set { frequency = (value >= 0.0 && value <= 10.0E3) ? value : 1000.0; }
        }
        public double Amplitude
        {
            get { return amplitude; }
            set { amplitude = (value >= 3.3 && value <= 15.0) ? value : 5.0; }

        }

    }
}







