using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indexadores2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Estante myEstante = new Estante(4);
            myEstante[0] = new Livros("Livro1", "Horror", 200.0);
            myEstante[1] = new Livros("Livro2", "supense", 200.1);
            myEstante[2] = new Livros("Livro3", "comedia", 200.2);
            myEstante[3] = new Livros("Livro4", "tragedia", 200.3);


            for(int i =0; i<4; i++)
            {
                Console.WriteLine($"{myEstante[i].Nome} de classificação {myEstante[i].Titulo} vale {myEstante[i].Valor}");
            }
                
        }
    }
}
