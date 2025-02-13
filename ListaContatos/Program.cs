using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ListaContatos
{
    class Program
    {
        static void Main(string[] args)
        {
            const int size = 4;
            VetorContatos conta = new VetorContatos(size);
            conta[0].Telefone = "34556454";
            conta[0].Nome = "lucas";
            Console.WriteLine($"{conta[0].Telefone}, {conta[0].Nome}");

        }
    }
}
