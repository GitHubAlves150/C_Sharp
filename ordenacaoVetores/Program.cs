using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordenacaoVetores
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] vetor = new int[] { 60, 55, 44, 76, 20, 100};

            for(int c=0; c<vetor.Length; c++)
            {
                Console.WriteLine(vetor[c]);
            }
            Console.WriteLine("\n------");
            Array.Sort(vetor);
            for (int c = 0; c < vetor.Length; c++)
            {
                Console.WriteLine(vetor[c]);
            }

            Console.WriteLine("\n--------");
            Console.WriteLine("arrays ordenados: " + string.Join(", ", vetor));

        }
    }
}
