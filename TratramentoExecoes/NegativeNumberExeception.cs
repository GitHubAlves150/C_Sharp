using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TratramentoExecoes
{
    internal class NegativeNumberExeception:ApplicationException
    {
        public NegativeNumberExeception()
            :base("Illegal operation for a negative number")
        {

        }
        public NegativeNumberExeception(string menssagem)
            : base(menssagem)
        {

        }

        public NegativeNumberExeception(string menssagem, Exception inner)
            : base(menssagem, inner)
        {

        }


    }//end NegativeNumberException
}
