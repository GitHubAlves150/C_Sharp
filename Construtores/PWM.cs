using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construtores
{
    class PWM
    {
        public PWM(double argDuty, double argFrequency, double argAmplitude)
        {
            SetPWM(argDuty, argFrequency, argAmplitude);
        }
        public PWM(double argDuty)
        {
            SetPWM(argDuty, 100.0, 5.0);
        }
        public PWM(double argDuty, double argFrequency)
        {
            SetPWM(argDuty, argFrequency, 5.0);
        }
        public PWM(PWM pwm)
        {
            SetPWM(pwm.duty, pwm.frequency, pwm.amplitude);
        }

                private double duty, frequency, amplitude;


        public void SetPWM(double argDuty, double ArgFrequency, double argAmplitude)
        {
            duty = (argDuty >= 0.0 && argDuty<=100.0) ? argDuty:50.0;
            frequency = (ArgFrequency >= 0.0 && ArgFrequency <= 10.0E3)? ArgFrequency : 1000.0;
            amplitude = (argAmplitude >= 3.3 && argAmplitude <= 15.0) ? argAmplitude : 5.0;
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

    }

}







