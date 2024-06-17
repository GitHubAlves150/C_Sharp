using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX005
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //A variavel a1 é do tipo "classe Animal" que setado como valor inicial o "construtor" 
            //Neste caso, o construtor é como se fosse atribuir zero numa variavel qualquer 
            Animal a1 = new Animal();
            Animal a2 = new Animal();
            Animal a3 = new Animal();
            Animal a4 = new Animal();
            Animal a5 = new Animal();

            int countCachorro = 0,
                countGato     = 0,
                countPeixe    = 0;
            for (int i=0; i<5; i++)
            {
                Console.WriteLine("Controle de animais.....");
                Console.WriteLine("Informe o primeiro nome do {0}º Animal..", i);
                a1.nome = Console.ReadLine();
                Console.WriteLine("Informe o tipo  Animal..");
                a1.Tipo = Console.ReadLine();
                //Console.WriteLine("Animal " + a1.nome);
                if (a1.Tipo == "Cachorro") 
                {
                    countCachorro++;
                }
                if (a1.Tipo == "Gato") 
                {
                    countGato++;
                }
                if (a1.Tipo == "Peixe") 
                {
                    countPeixe++;
                }
            }
            

             


            a1.ExibirDados();
            Console.WriteLine("Quantidade de Cachorro: " + countCachorro);
            Console.WriteLine("Quantidade de Gato: "     + countGato);
            Console.WriteLine("Quantidade de Peixe: "    + countPeixe);
            Console.ReadKey();
    
        }
    }
}
