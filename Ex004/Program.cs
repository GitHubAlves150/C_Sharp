/*
programa: Calsse pessoa que calcula idade

Conceito deste programa é trabalhar com a classe pessoa que calcula pega 
o nome e ano de nascimento e calcula a idade, usando apenas propriedade
get ou set.
 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex004
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Pessoa Gustavo;
            Gustavo = new Pessoa();
            Console.WriteLine("Digite seu nome...");
            Gustavo.Name=Console.ReadLine();
            Console.WriteLine("Digite o ano de nascimento...");
            Gustavo.Anonascimento = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(Gustavo.Idade);
            //Gustavo.ExibirDados();           
            Console.ReadKey();  
                             
        }
    }
}
