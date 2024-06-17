using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex003
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Pessoa Lucas = new Pessoa();
            Lucas.Name= "Lucas";
            Lucas.Anonascimento = 1948;
            Lucas.ExibirDados();
            Console.WriteLine("Digite o nome da pessoa.....");
            Lucas.Name=Console.ReadLine();
            Console.WriteLine("Digite o ano de nascimento..");
            Lucas.Anonascimento=Convert.ToInt32(Console.ReadLine());
            Lucas.ExibirDados();    

            for (; ; ) { }

        }
    }
}
