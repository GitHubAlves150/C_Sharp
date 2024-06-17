/*
Programa: Construtor parte 3
Autor: Lucas Lorenço Alves
 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex008
{
    public class Program
    {
        static void Main(string[] args)
        {
            Pessoa jovem=new Pessoa();//instanciado o objeto jovem e inicializado com os parametros do construtor
            //parametros default
            jovem.Exibe();
            jovem.nome("Estudante");
            jovem.Exibe();

            //Prende a tela do console
            Console.ReadKey();
        }//Fim do programa principal

    }//

}//Fim da inclusao da lib ou comoo dito o namespace
