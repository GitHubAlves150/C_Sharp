
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace indexadores
{
    class Prateleira
    {
        public double Valor{get; set; }
        public string Nome { get; set; }

        public Prateleira(string _nome, double _valor)
        {
            Valor = _valor;
            Nome = _nome;
        }
    }
}
