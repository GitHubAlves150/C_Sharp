using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaContatos
{
    class ListaContato
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }

       
        ListaContato(string nome, string telefone)
        {
            Nome = nome;
            Telefone = telefone;
        }


    }
}
