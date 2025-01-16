using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversaoEstreitamento
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (display(-58) == -1)
                Console.WriteLine("ERRO");
            
            Console.ReadLine();
        }

        static void Argint(int arg)
        {

            Console.WriteLine(arg);
        }
        static void ArgFloat(float arg)
        {
            Console.WriteLine(arg);
        }
        static int display(int value)
        {
            if(value < 0)
            {
                return -1;
            }
            char dig1 = (char)(value / 100 + 48);
            char dig2 = (char)( (value % 100/10) + 48);
            char dig3 = (char)(value % 100 + 48);

            Console.WriteLine("Hundreds: " + dig3);
            Console.WriteLine("Tens: " + dig2);
            Console.WriteLine("Units: " + dig1);
            return 0;
        }
        
    }
}
