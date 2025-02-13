

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace indexadores
{
    class vetor
    {
        private Prateleira[] arrproduto;

        public vetor(int size)
        {
            arrproduto = new Prateleira[size];
        }
        //indexador
        public Prateleira this[int index]
        {
            get { return arrproduto[index]; }
            set { arrproduto[index] = value;  }
        }

    }
}
