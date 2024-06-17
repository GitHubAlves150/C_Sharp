/*
 * Programa: Passagem por referencia
 * Autor: Lucas Lorenço Alves
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passagem_Por_Referencia
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int num = 20;
            dobrar(ref num);   //Passam por referencia
            Console.WriteLine("Valor:" + num);
            Console.ReadKey();  

        }



        static void dobrar(ref int valor)
        {
            //valor = valor * 2;
            valor *=2;
        }

    }
}
