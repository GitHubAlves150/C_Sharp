using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Thread_StopWatch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            // ExecutarSequencial();
            ExecutuarComthread();
            
            Console.WriteLine($"Operacao gastou {sw.ElapsedMilliseconds} milisegundos");

        }
        static void RealizarOperacao(int operacao, string nome, string sobrenome)
        {
            Console.WriteLine($"Iniciando a operacao {operacao}...");
            for(int i=0; i<1000000000; i++)
            {
                var p = new Pessoa(operacao, nome, sobrenome);
            }

            Console.WriteLine($"Operacao finalizada {operacao}");

        }

        static void ExecutarSequencial()
        {
            RealizarOperacao(1, "Lucas", "Alves");
            RealizarOperacao(2, "Henrique", "Gonçalves");
            RealizarOperacao(3, "Rubia", "Dilema");
        }
        static void ExecutuarComthread()
        {
            var t1 = new Thread(() =>
            {
                RealizarOperacao(1, "Lucas", "Alves");
            });

            var t2 = new Thread(() =>
            {
                RealizarOperacao(2, "Henrique", "Gonçalves");
            });

            var t3 = new Thread(() =>
            {
                RealizarOperacao(3, "Rubia", "Dilema");
            });
            t1.Start();
            t2.Start();
            t3.Start();

            t1.Join();
            t2.Join();
            t3.Join();
        }

    }
}
