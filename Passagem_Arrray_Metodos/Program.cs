using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passagem_Arrray_Metodos
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int[] vetor = new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            int[] vectorCopy = new int[10];
            vectorCopy= vetor;
            

            PrintVetor("Atual", vetor);
            Console.WriteLine("________________________\n");

            Updatenumbers( vetor);
            PrintVetor("Updatenumbers",  vetor);
            Console.WriteLine("________________________\n");
            PrintVetor("Copia: ", vectorCopy);
            Console.WriteLine("________________________\n");
/*

            UpdatenumbersRef(ref vetor);
            PrintVetor("UpdatenumbersRef", vetor);
            Console.WriteLine("________________________\n");

            PrintVetor("Atual novamente", vetor);
            Console.WriteLine("________________________\n");

            PrintVetor("Copia: ", vectorCopy);
*/


        }

        static void Updatenumbers( int[] n)
        {
            for(int i=0; i< n.Length; i++)
            {
                n[i]= n[i] * n[i];
                
            }

        }
        static void UpdatenumbersRef(ref int[] n)
        {
            for (int i = 0; i < n.Length; i++)
            {
                n[i]=n[i] * 2;
            }
        }

        static void PrintVetor(string _string, int[] vetor)
        {
            for(int i=0; i<vetor.Length; i++)
            {
                Console.WriteLine(_string + vetor[i] );
            }
        }




    }
}
