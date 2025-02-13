using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tipos_de_excecoes
{
    public class numeroNegativo: Exception
    {
        public string execao;
       public numeroNegativo(string msn):base(msn)
        {
            execao = msn;
        }
    }
}
