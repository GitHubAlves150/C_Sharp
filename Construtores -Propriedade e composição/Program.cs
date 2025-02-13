using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construtores
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PWM pwm1= new PWM(75.0);
            pwm1.Frequency = 5000.0;
            
            pwm1.Report("PWM1");
            pwm1.Duty = 20.0;
            pwm1.Amplitude = 3.3;
            pwm1.Report("pwm1");


        }
    }
}
