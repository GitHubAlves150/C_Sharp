using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpV8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Precione Enter para continuar");
            Console.ReadLine();
            var watch = Stopwatch.StartNew();
            //invocar metodos que vamos executar
            Parallel.Invoke(
                     new Action(mes),
                     new Action(dias)
                );

            watch.Stop();
            Console.WriteLine($"It took {watch.Elapsed.Seconds} segundo(s) to complete.");
        }
        static void mes()
        {
            string[] mes = { "Janeiro", "Fevereiro", "Março", "Abril" };

            foreach (string month in mes)
            {

                Console.WriteLine(month);
                Thread.Sleep(2000);
            }
        }

        static void dias()
        {
            string[] dias = { "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sabado", "Domingo" };
            foreach (string days in dias)
            {
                Console.WriteLine(days);
                Thread.Sleep(2000);
            }
        }


    }


}
