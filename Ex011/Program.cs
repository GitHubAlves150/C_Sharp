/*
Programa: Bip para cronometrar o tempo de um pitch
Autor: Llucas Lorenço Alves
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex011
{
    public class Program
    {
        static void Main(string[] args)
        {
            int Vartempo = 0;
            Alarme alarme = new Alarme();


            Console.WriteLine("A hhora do pitch..");
            Console.WriteLine("Informe a duração do pitch.");
            Vartempo = Convert.ToInt32(Console.ReadLine() );
            alarme.Tempo = Vartempo;
            alarme.iniciar();
           // Console.ReadKey();

        }
    }
}
