using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sobre_Carga
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine(teste(2));
            Console.WriteLine(teste(2.96));
            Console.WriteLine(teste(2.96, 2));
            Console.WriteLine(teste(2.96, 2, 6.2f));
            Console.ReadLine();
        }

        static int teste(int a)
        {
            
            return a;
        }
        static double teste(double d)
        {
            return d;
        }

        static double teste(double d, int f)
        {
            return d+f;
        }
        static double teste(double d, int f, float r)
        {
            return d + f + r;
        }
    }
}
