using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] vetor= new int[] { 1, 2, 3 };
            int[] copy = (int[])vetor.Clone();

            modifica(copy);
            Console.WriteLine("Array original: " + string.Join(", ", vetor));
            Console.WriteLine("Array Modificado: " + string.Join(", ", copy));



            //Array.Copy(vetor, copy, vetor.Length);

            /*   printvetor(vetor);
               Console.WriteLine("\n----");
               modifica(vetor);
               Console.WriteLine("\n----");
               printvetor(copy);
               Console.WriteLine("\n----");
            */
        }


        static void modifica(int[] vet)
        {
            for (int i = 0; i < vet.Length; i++)
            {
                vet[i] = i * 2;
               // Console.Write(", " + vet[i]);
            }
        }
        static void printvetor(int[] vet)
        {
            for(int i =0; i<vet.Length; i++)
            {
                Console.Write(", " + vet[i]);
            }
        }
    }
}
