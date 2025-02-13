using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thread_StopWatch
{
    public class Pessoa
    {
        private string _nome;
        private string _sobrenome;
        private int _op;

        public Pessoa(int op, string nome, string sobrenome)
        {            
            _op = op;
            _nome = nome;
            _sobrenome = sobrenome;
        }

    }
}
