using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construtores_de_String
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] array = {'1', '2', '3', 'a', 'w', '4', 'd', 'v', 'f' };
            string str0, str1, str2, str3;
            str0 = "String 0";
            str1 = "String 1"; 


            str1 = new string(array);
            str2 = new string(array, 3, 2);

            Console.WriteLine("Result: " + str2);
            Console.WriteLine("Str1: "+ str1);


        }



    }
}
