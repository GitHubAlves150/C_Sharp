using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Multithreading_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Thread test1 = new Thread(Test1);
            Thread test2 = new Thread(Test2);
            Thread test3 = new Thread(Test3);
            test1.Start(); test2.Start(); test3.Start();

            //Test1();
            // Test2();
            // Test3();

        }


        static void Test1()
        {
            for(int i=1; i<100; i++)
            {
                Console.WriteLine("teste1: "+ i);
                if( i %2 == 0)
                {
                    Console.WriteLine("Console Test1 is sleeping");
                    Thread.Sleep(5000);
                }
            }

        }//end
        static void Test2()
        {
            for (int i = 1; i < 100; i++)
            {
                Console.WriteLine("teste2: " + i);
                if (i % 2 == 0)
                {
                    Console.WriteLine("Console Test2 is sleeping");
                    Thread.Sleep(5000);
                }
            }
        }//end
        static void Test3()
        {
            for (int i = 1; i < 100; i++)
            {
                Console.WriteLine("teste3: " + i);
                if( i % 2 == 0)
                {
                    Console.WriteLine("Console Test3 is sleeping");
                    Thread.Sleep(5000);
                }
            }
        }//end




    }
}
