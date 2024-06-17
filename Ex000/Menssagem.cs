using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe00
{
    public class Menssagem
    {
       
       public String textoMenssagem;

       public void ExibirMenssagem()
        {
            Console.WriteLine(textoMenssagem);
        }

        public void setMenssagem(string _string)
        {

            textoMenssagem = _string.ToUpper();
        }

        public string getMenssagem()
        {
            return textoMenssagem;
        }



    }
}
