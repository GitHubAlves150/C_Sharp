using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indexadores2
{
    class Livros
    {
        private string _Nome;
        private string _Titulo;
        private double _Valor;

        public Livros(string _nome, string _titulo, double _valor)
        {
            Nome = _nome;
            Titulo= _titulo;
            Valor = _valor;
        }

        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }

        public string Titulo
        {
            get { return _Titulo; }
            set { _Titulo = value; }
        }

        public double Valor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }


    }
}
