using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe00
{
    internal class Program
    {
        static void Main(string[] args)
        {
           //Console.WriteLine("Ola");
            Menssagem msg1, msg2;
            //msg1 = new Menssagem();
            //msg1.textoMenssagem="opaa...";
            //
            msg2 = new Menssagem();
            
            msg2.setMenssagem("Lucas Alves");
            msg2.ExibirMenssagem();


            Console.Read();

        }
    }
}
