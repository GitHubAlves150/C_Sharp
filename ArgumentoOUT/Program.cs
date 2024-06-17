/*
 * Programa: Arumento OUT, para retornar mais de um valor
 * È possível retornar mais de um ou dois valores se usar referência
 * Mas! a técnica "out" também serve.
 * 
 * Autor: Lucas Lorenço Alves
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArgumentoOUT
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int _divide, _dividendo, _quociente, _resto;
            _divide = 11;
            _dividendo = 5;
            _quociente = divisor(_divide, _dividendo, out _resto);

            Console.WriteLine("{0}/{1}: quociente={2} e resto={3}", _divide, _dividendo, _quociente, _resto);
            Console.ReadKey();

        }


        static int divisor(int dividendo, int divisor, out int resto)
        {
            int quociente;
            quociente = dividendo / divisor;
            resto = dividendo % divisor;

            return quociente;
            
        }
    }
}
