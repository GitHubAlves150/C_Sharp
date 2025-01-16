using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            msg();
            Console.WriteLine(valor(30));

            Console.ReadLine();
        }//End Main


        static void msg()
        {
            Console.WriteLine("Ola");
        }
        static float valor(float f)
        {
            return f + 20;
        }
    }
    
}
