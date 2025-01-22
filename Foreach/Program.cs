using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foreach
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //foreach-Lista e dicionarios
            int[] number = new int[] { 1, 2, 3, 4, 5};

            foreach (int value in number)
            {
                Console.WriteLine(" " + value);
            }

            int[,] matrix = {
                            {1, 2, 3 },
                            {4, 5, 6 },
                            { 7, 8, 9}            
                            };
            int max = matrix[0, 0];
            foreach(int numb in matrix)
            {
                if(numb > max)
                {
                    max = numb;
                }

            }
            Console.WriteLine("max value: " + max);
        }
    }
}
