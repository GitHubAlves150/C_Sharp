using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heranca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TelescopioSplit Tel_zor = new TelescopioSplit("ZORBIUM", 1001.0, 46.0);

            Console.WriteLine($"{Tel_zor.NOME_} tem  {Tel_zor.Zoom} de zoom e {Tel_zor.Angulo} de angulo");


        }
    }
}
