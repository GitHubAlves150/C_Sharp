using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaContatos
{
    class VetorContatos
    {
        public ListaContato[] contato;

        public VetorContatos(int size)
        {
            contato = new ListaContato[size];
        }
        //indexadores
        public ListaContato this[int index]
        {
            get { return contato[index]; }
            set { contato[index] = value; }
        }


    }
}
