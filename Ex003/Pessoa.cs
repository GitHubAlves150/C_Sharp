using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Ex003
{
    public class Pessoa
    {
       

        private int     _AnoNascimento;
        private string  _Name;


        //controle do ano de nascimento
        public int Anonascimento 
        {           
            
            get
            {
                return this._AnoNascimento;
            }
            set
            {
                this._AnoNascimento = value;
            }

        }

        //Controle da string
        public string Name
        {           
           
            get
            {
                return _Name;
            }
            set
            {
                _Name = value.ToUpper();
            }

        }

        public void ExibirDados()
        {

            Console.WriteLine("Nome " + _Name);
            Console.WriteLine("Idade " + CalcularIdade());
            

        }


        private int CalcularIdade()
        {

            DateTime data = DateTime.Now;
            int ano= data.Year;
            int idade=ano-this._AnoNascimento;

            return idade;
        }

    }
}
