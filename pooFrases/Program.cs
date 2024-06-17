
/*
Programa:Perguntas e respostas
Autor Lucas Lorenço Alves
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pooFrases
{
    public class Program
    {
        static void Main(string[] args)
        {
            PerguntaPlus p = new PerguntaPlus();
           // p.Texto    = "Qual é o mecanismo de busca mais famoso..";
            p.Resposta = "Google";
            p.Dica     = "PErgunta pro Go...e";
            Console.WriteLine("Qual é o mecanismo de busca mais famoso..");
            Console.WriteLine("Dica: " + p.Dica);
            String resposta =Console.ReadLine();

           if(p.Resposta.ToUpper() == resposta.ToUpper() )
            {
                Console.WriteLine("Acertou..\n Desde de 2000 o buscador Google é um dos mais requisitados na internet"
                                   + "\nE até hoje o GooGle é excelente para pesquisas");
            }
           else
            {
                Console.WriteLine("Errou...");
                Console.WriteLine("A resposta correta é: " + p.Resposta);
            }
            
            Console.ReadKey();

        }
    }
}
