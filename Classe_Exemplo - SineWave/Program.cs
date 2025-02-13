using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classe_Exemplo
{
    
    internal class Program
    {
        static void Main(string[] args)
        {

            FunctionGen sig1, sig2;

            sig1 = new FunctionGen(2.0, 1500.0, 10.0, false, false);
            sig2 = new FunctionGen(75.0, 900.0, 8.0);

            sig1.setSwep(1, 1000, 20);

            sig1.SetFirmwareDate(2024, 6, 10);
            sig2.SetFirmwareDate(2024, 2, 15);

            sig1.Report("sig1");
            sig2.Report("sig2");


            Console.WriteLine("\n end of Program");
            Console.ReadLine();

        }
    }
}
