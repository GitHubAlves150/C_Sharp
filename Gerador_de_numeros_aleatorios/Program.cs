using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerador_de_numeros_aleatorios
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random randomico = new Random();//sem parametros o visual studio configura os numeros de acorod como relogio do sisitema
            int value = randomico.Next(100);
            Console.WriteLine(value);
            Console.ReadLine();
        }
    }
}


















