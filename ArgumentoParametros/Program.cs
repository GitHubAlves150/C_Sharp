/*
 * Programa: Passar mais de um valor como parametro 
 * usando o params tipo[]variavel
 * static int soma(params int[]n)
 * Ao invés de soma(int n1, int n2, int n3, ...) 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArgumentoParametros
{
    internal class Program
    {
        static void Main(string[] args)
        {
            soma(2, 2, 10, 44, 3, 21);
            Console.ReadKey();
        }

        static void soma(params int[]n)
        {
            int resto =0;
            if(n.Length <1)
            {
                Console.WriteLine("Não existem valores a serem somados...");

            }
            else if(n.Length < 2)
            {
                Console.WriteLine("Valores insuficiente para soma: {0}", n[0]);
            }
            else
            {
                //foreach(int i in n)
                for (int i=0; i < n.Length; i++) 
                {
                    resto += n[i];
                }
                Console.WriteLine("A soma dos valores é: {0}", resto);
            }

            
        }



    }
}
