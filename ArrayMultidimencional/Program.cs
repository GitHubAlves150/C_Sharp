using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayMultidimencional
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] arr1 = new int[3,2];
            arr1[0,0] = 1; arr1[0,1] = 2; 
            arr1[1,0] = 4; arr1[1,1] = 5; 
            arr1[2,0] = 9; arr1[2,1] = 10;

            int[,] arr2 = { { 01, 02, 03, 04},
                            { 10, 20, 30, 40},
                            { 11, 21, 31, 41},
                            { 14, 24, 34, 44},
                            { 17, 27, 37, 47}
                          };


            
            
            int cal = arr1[1,0] * arr1[2,0];
            Console.WriteLine("\n\n"+cal);
            int[][] arr4 = new int[4][];
            
            arr4[0] = new int[] {1, 2, 3, 4 };
            arr4[1] = new int[] { 22, 4, 3, 2, 5, 6};
            arr4[2] = new int[] { 33, 5, 4 };
            arr4[3] = new int[] { 2, 3};

            printMultArray(arr4, "array4");

        }
        static void printMultArray(int[,] array, string arrayName)
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);
            Console.WriteLine("\t=====" + arrayName + " ======");
            for(int c=0; c<rows; c++)
            {
                for(int j=0; j<cols; j++)
                {
                    Console.Write("\t" + array[c, j]);
                }
                Console.WriteLine();
            }
        }

        //sobrecarga de methodo
        static void printMultArray(int[][] array, string arrayname)
        {
            Console.WriteLine("\t=====" + arrayname + " ======");
            for(int c=0; c<array.Length; c++)
            {
                for(int j=0; j < array[c].Length; j++)
                {
                    Console.Write("\t"+array[c][j]);
                }
                Console.WriteLine();
            }
        }


    }
}
