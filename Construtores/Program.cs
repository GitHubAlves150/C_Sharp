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
            PWM pwm1= new PWM(125.0, 1000E3, 45.0);
            PWM pwm2 = new PWM(15.0);//100.0, 5.0
            PWM pwm3 = new PWM(75.0, 2000.0);//5.0
            PWM pwm4 = new PWM(pwm1);

            pwm1.Report("PWM1");
            pwm2.Report("PWM2");
            pwm3.Report("PWM3");
            pwm4.Report("PWM4");


        }
    }
}
