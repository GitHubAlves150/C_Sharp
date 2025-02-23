using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadCSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {   
            Console.WriteLine("Atividades do Lucas");
            Thread mythread = new Thread(Dowork );
            Thread mythread1 = new Thread(Dowork1);
            mythread.Name = "Worker Thread";
            mythread.Start();
            mythread1.Start();

           
                
               

        }

        static void Dowork()
        {
            for(int c=0; c<15; c++)
            {
                Console.WriteLine("Lucas estudando....");
                Thread.Sleep(5000);
            }
        }

        static void Dowork1()
        {
            for (int c = 0; c < 15; c++)
            {
                Console.WriteLine(" Lucas trabalhando....");
                Thread.Sleep(5000);
            }
        }
    }
}
