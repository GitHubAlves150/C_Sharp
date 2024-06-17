using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex001
{
    public class Program
    {
        static void Main(string[] args)
        {
            Menssagem Msg01 = new Menssagem();
            Msg01.TextMenssagem = "LUCAS";
            Msg01.ExibirMenssagem();


            Console.ReadLine();
        }
    }
}
