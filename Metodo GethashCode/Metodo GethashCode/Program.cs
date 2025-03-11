using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metodo_GethashCode
{
    class Program
    {
        static void Main(string[] args)
        {
            string str1, str2, output;
            str1 = "Internet ";
            str2 = "das coisas";

            output = " The hash code for \"" + str1 + " \" is " + str1.GetHashCode() + '\n';
            output += " The hash code for \"" + str2 + " \" is " + str2.GetHashCode() + '\n';

            Console.WriteLine(output);

            Console.WriteLine("\n End of Program.");
            Console.ReadLine();

        }
    }
}
