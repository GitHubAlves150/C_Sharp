using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passagem_por_valor_e_porreferencia
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int quociente=1;
            int a=40, b=2;
            teste(a, b, ref quociente);

            Console.WriteLine("Quociente: "+ quociente);
        }
        static int teste(int d, int e, ref int quociente)//se for out, troca ref por out, e não precisa inicializar a variavel quociente
        {
             quociente = 0;
            if (e == 0 )
            {
                quociente = -1;

            }
            else
            {
                quociente = (d / e);
                
            }
            return 1;
            
        }
    }
}
