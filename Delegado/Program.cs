using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegado
{
    
    internal class Program
    {
        static void Main(string[] args)
        {
            CalcularDoisNumeros calc_1 = new CalcularDoisNumeros();
            calc_1.soma(2, 3);

            returnOperation dele_1 = new returnOperation(calc_1.subtrair);
            int result = dele_1(10, 3);

            Console.WriteLine($">>{result}");

            Console.ReadLine();
        }
      
        static int ret(int h, int r)
        {
            return h + r;
        }
    }
}



