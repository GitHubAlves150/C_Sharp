/*
 * Programa: trabalhando com Enum
 * Autor: Lucas Lorenço Alves
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enum
{
    public class Program
    {
        enum mes { Jan=1, Fev, Mar, Abr, Mai, Jun, Jul, Ago, Set, Out, Nov, dez};

        static void Main(string[] args)
        {
            mes aniversario = mes.Jan;
            Console.WriteLine("inicio de ano é  " + aniversario);

            aniversario = (mes)8;
            Console.WriteLine("Mes de aniversario " + aniversario );
            Console.WriteLine("Mes de aniversario " + aniversario +"sto");


            Console.ReadKey();
        }
    }
}
