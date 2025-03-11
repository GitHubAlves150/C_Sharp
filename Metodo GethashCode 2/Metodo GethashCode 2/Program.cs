using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metodo_GethashCode_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string information = "Internet das coisas";
            char[] searchLetter = { 'I', 'r', 'c', 's'};

            Console.WriteLine(information.IndexOf('r', 2, 10));
            Print(information.IndexOf('e'));
            //Print(information.IndexOf('r', 1));
            //Print(information.IndexOf('o', 8, 2));
        }

        static void Print(int result)
        {
            Console.WriteLine(" " + result);
        }//End Print

        static void Print(int result, char addChar)
        {
            Console.WriteLine(" " + result + addChar);
        }//End Print
    }
}












