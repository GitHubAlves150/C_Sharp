using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tratamento_Excecao
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for(; ; )
            {
                try
                {
                Console.WriteLine("enter With an integer number1: ");
                int number1 = int.Parse(Console.ReadLine());
                
                Console.WriteLine("Enter with a integer number2: ");
                int number2 = int.Parse(Console.ReadLine() );

                int sum =unchecked(number1 + number2);
                
                Console.WriteLine("{0} + {1} = {2}", number1, number2, sum);

                }
                catch(OverflowException errado)
                {
                    Console.WriteLine("Error: Number Overflow");
                    Console.WriteLine("Error Report" + errado.Message);
                }
                catch(FormatException)
                {
                    Console.WriteLine("Error: Enter with intgers values");
                }

                finally
                {
                    Console.WriteLine("\n END of program");
                }
            }
            
            
        }
    }
}
