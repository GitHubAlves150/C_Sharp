/*
Programa: Construtor parte 1
Autor: Lucas Lorenço Alves
 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ex006 //Aqui é o mesmo que o include "LIB_H" de biblioteca no C++
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player LetsPlayer;
            LetsPlayer=new Player();//inicializo a variavel do tipo classe Player com os parametros do construtor
            LetsPlayer.Chao="Piso";
            Console.WriteLine(LetsPlayer.Chao);
            Console.ReadKey();
        }
    }
}
