using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace indexadores
{
    internal class Program
    {
        static void Main(string[] args)
        {
            vetor arrProd = new vetor(4);
            arrProd[0] = new Prateleira("melado", 3.66);
            arrProd[1] = new Prateleira("feijao", 2.66);
            arrProd[2] = new Prateleira("arroz", 5.66);
            arrProd[3] = new Prateleira("brigadeiro", 1.66);


            for (int i=0; i < 4; i++)
            {
                //Console.WriteLine($"{arrProd[i].Nome} com valor de {arrProd[i].Valor}");
                Console.WriteLine(arrProd[i].Nome + " com valor de " + arrProd[i].Valor);
            }
        }
    }
}
