using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metodo_GetHashCode_3
{
    class Program
    {
        static void Main(string[] args)
        {
            string infomation = "Internet das coisas é muito bom!";

            Console.WriteLine(infomation);
            Console.WriteLine(infomation.Substring(2, 5));

            string str2 = " è o que eu estudo";
            Console.WriteLine(infomation + str2);
            Console.WriteLine("Concatenando..."+String.Concat(infomation, str2));

            Console.WriteLine("End of program");
            Console.ReadLine();
        }
    }
}
