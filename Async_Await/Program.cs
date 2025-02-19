//https://www.youtube.com/watch?v=jfuODQqYFJU

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Async_Await
{
    internal class Program
    {

        static async Task PassarCafe()//se ela for retornar uma task tem que colcoar <int> depois do task; Task<int>
        {
           await Task.Run(() =>
            {
            for(int c=0; c<10; c++)
            {
                Console.WriteLine($"passando café {c}");
                Thread.Sleep(1000);
            }
            Console.WriteLine("Café Pronto");
            });
                
            
        }

        static void Tostarpao()
        {
            Task.Run(() =>
            {
                for (int c = 0; c < 5; c++)
                {
                    Console.WriteLine($"Tostando Pão {c}");
                    Thread.Sleep(1000);

                }
                Console.WriteLine("Pão Pronto");

            });

        }
        static void Main(string[] args)
        {
            
            
            
           Task taskPassarCafe = PassarCafe();
           Tostarpao();
           taskPassarCafe.Wait();

           Console.WriteLine("CAFÉ NA MESA");
           
           Console.ReadLine();
        }
    }
}
