

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indexadores2
{
    class Estante
    {
        private Livros[] arrLivros;

        public Estante(int size)
        {
            arrLivros= new Livros[size];
        }
        //indexadores
        public Livros this[int i]
        {
            get { return arrLivros[i]; }
            set { arrLivros[i] = value; }
        
        }


    }
}
