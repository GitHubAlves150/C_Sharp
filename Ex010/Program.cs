/*
Programa: Calculo da área de uma figura de 4 lados (Quadrado/Retangulo)
Autor: Lucas Lorenço Alves
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex010
{
    public class Program
    {
        static void Main(string[] args)
        {
            Double var1, var2;

            Retangulo Figura;
            Figura = new Retangulo(); // Inicializa com os parâmentros do construtor

            Console.WriteLine("Valores menores que zero...");
            Figura.Area_Ret_Quad(0.0f, 0.0f);
            Figura.Exibe();

            Console.WriteLine("Digite um valor para a base....");
            var1 = Convert.ToDouble(Console.ReadLine() );

            Console.WriteLine("Digite um valor para a base....");
            var2 = Convert.ToDouble(Console.ReadLine());

            Figura.Area_Ret_Quad(var1, var2);
            Figura.Exibe();
            Console.WriteLine("Fim...");
            Console.ReadKey();
        }
    }
}
