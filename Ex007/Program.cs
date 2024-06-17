/*
Programa: Construtor parte 2
Autor: Lucas Lorenço Alves
 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex007 //Inclusao da lib local, no C++ seria #inlcude "LIB_LOCAL"
{
    public class Program
    {
        static void Main(string[] args)
        {

            Pessoa Famosa=new Pessoa("Gabriela", 33);
            Famosa.Exibe();
            Console.ReadKey();
        }
    }
}
